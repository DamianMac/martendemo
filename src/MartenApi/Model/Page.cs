namespace MartenApi.Model;

public class Page<T>
{
    public Page(IEnumerable<T> data, int total, int totalPages, int currentPage)
    {
        Total = total;
        TotalPages = totalPages;
        CurrentPage = currentPage;
        Data = data;
    }

    public int Total { get; private set; }

    public int TotalPages { get; private set; }

    public int CurrentPage { get; private set; }

    public IEnumerable<T> Data { get; private set; }
}