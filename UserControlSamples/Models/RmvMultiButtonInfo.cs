namespace UserControlSamples.Models
{
    public class RmvMultiButtonInfo
    {
        public string Text { get; set; }

        public string Key { get; set; }

        public string ImageKey { get; set; }

        public int Order { get; set; }

        public static implicit operator RowButonInfo(RmvMultiButtonInfo dto)
        {
            var row = new RowButonInfo()
            {
                Key = dto.Key,
                Text = dto.Text,
                ImageKey = dto.ImageKey
            };
            return row;
        }
    }
}
