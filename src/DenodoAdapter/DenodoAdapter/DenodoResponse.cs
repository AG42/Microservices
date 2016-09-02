namespace DenodoAdapter
{
    public class DenodoResponse<T>
    {
        public string name { get; set; }
        public T elements { get; set; }
    }
}
