namespace Server.model
{
    public class ModelGiaoVien:ModelNguoi
    {
        public virtual int Id { get; set; }
        public virtual string MaGiaoVien { get; set; } = "";
    }
}
