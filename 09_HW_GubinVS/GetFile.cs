/// <summary>
/// Класс содержащий поля при десериализации json - ответ на запрос getFile
/// получение поля file_path
/// </summary>
public class GetFile
{
    public bool ok { get; set; }
    public Result result { get; set; }
}

public class Result
{
    public string file_id { get; set; }
    public string file_unique_id { get; set; }
    public int file_size { get; set; }
    public string file_path { get; set; }
}
