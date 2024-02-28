namespace BlazorCRUDApp
{
    public class ChartObj
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public ChartObj() { }
        public ChartObj(string key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}
