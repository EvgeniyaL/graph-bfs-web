namespace GraphBfs.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is CityDto)
            {
                return this.Id.Equals((obj as CityDto).Id);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
