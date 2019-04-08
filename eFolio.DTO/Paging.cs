namespace eFolio.DTO
{
    public class Paging
    {
        public Paging(int from, int size)
        {
            From = from;
            Size = size;
        }

        public int From { get; set; }
        public int Size { get; set; }
    }
}
