# Building a full text search


Step 1. Read all of this

https://rachbelaid.com/postgres-full-text-search-is-good-enough/


Step 2. Create a materialised view with the properties and weights.

```

DROP MATERIALIZED VIEW IF EXISTS search_index;

CREATE MATERIALIZED VIEW search_index AS
SELECT
    id,
    coalesce((data->'Properties'->>'title'), (data->>'Name')) as title,
    data->'Properties'->>'synopsys' as synopsys,
    substring(slug from '\d{4}') as year,
    coalesce((data->'Properties'->>'headerImage'), (data->'Properties'->>'photo')) as headerImage,
    setweight(to_tsvector((coalesce((data->'Properties'->>'title'), (data->>'Name')))), 'A')
        || ' ' ||
    setweight(to_tsvector (coalesce((data ->'Properties'->>'synopsys'), '')), 'B')
        || ' ' ||
    setweight(to_tsvector (coalesce((data ->'Properties'->>'body'), '')), 'C')
        as document
FROM mt_doc_document;

CREATE INDEX idx_fts_search ON search_index USING gin(document);


```

Step 3. Build something to refresh it on a timer

```

using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "REFRESH MATERIALIZED VIEW search_index;";
                cmd.ExecuteNonQuery();
                conn.Close();
            }  

```

Step 4. Query it, joining to Marten tables as necessary

```

SELECT 
	            d.id, d.path, slug, year,
	            m.data->'Properties'->>'umbracoFile' as image,
	            i.title, 
	            i.synopsys,
	            d.content_type_alias as contenttype, content_sub_type as contentsubtype
	            
            FROM search_index i
            INNER JOIN mt_doc_document d ON d.id = i.id
            LEFT JOIN mt_doc_media m ON m.id = i.headerImage::int
            WHERE
	            d.path like $1 AND
	            d.published = true and 
	            d.content_type_alias = $4 AND
	            document @@ to_tsquery($2)	
            ORDER BY 
	            year desc,
	            ts_rank(document, to_tsquery($2)) DESC

```