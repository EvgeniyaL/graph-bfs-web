namespace GraphBfs.Dtos
{
    public class PathDto
    {
        public int Id { get; set; }

        public CityDto InitialCity { get; set; }

        public CityDto EndCity { get; set; }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
