    public class Message
    {
        public int message_id { get; set; }

        public From from { get; set; }

        public Chat chat { get; set; }

        public int date { get; set; }

        public string text { get; set; }

        public Document document { get; set; }

        public Sticker sticker { get; set; }      
        
        public Photo[] photo { get; set; }

        public Voice voice { get; set; }

        public Video_Note video_note { get; set; }

}

