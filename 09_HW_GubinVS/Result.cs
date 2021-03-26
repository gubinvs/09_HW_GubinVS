public class Result
{
    public int update_id { get; set; }
    public string file_id { get; set; }
    public string file_unique_id { get; set; }
    public int file_size { get; set; }
    public string file_path { get; set; }
    public Message message { get; set; }

    public Voice voice { get; set; }
}
