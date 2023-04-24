using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetPracticesDatabase
{
    internal class Program
    {


        // static SqlConnection con = new SqlConnection(data source,inital catalog,user,password) bağlantı yapıcaksın.
        // bağlantıyı bir kez yapıyorum ve static kullanarak her yerde kalıcı olmasını sağlıyorum.
        // bu sayede her seferinde con bağlantısını yazmak yerine sadece 'con' değişkenimi çağırarak halletmiş oluyorum.


        static void Main(string[] args)
        {
            // static tipinde methodlar oluşturuyorum ki kodum hem okunabilir olsun hem de bu methodu nerede çağırırsam çalışsın. 
      


        }

        public static void LoginKayitSil(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from loginTable where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            int donenDeger = cmd.ExecuteNonQuery();
            if (donenDeger == 1)
            {
                Console.WriteLine("kayit silindi...");
            }
            else
            {
                Console.WriteLine("kayit silinemedi...");
            }

            con.Close();
        }
        public static void LoginKayitGuncelle(int id, string kullaniciAdi)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("uptade from loginTable set kullaniciAdi=@kullaniciAdi where id=@id", con);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@id", id);
            int donenDeger = cmd.ExecuteNonQuery();
            if (donenDeger == 1)
            {
                Console.WriteLine("kayıt güncellendi...");
            }
            else
            {
                Console.WriteLine("kayıt güncellenemedi...");
            }
            con.Close();
            Console.ReadLine();

        }
        public static void MusteriKayıtlariniGetir()
        {
            List<Musteri> musteriList = new List<Musteri>();
            // musteri tipinde bir generic class oluşturdum.
            // sqldatareader ile okuduğum değerleri bu liste içinde döndürerek yazıcam.


            con.Open();  // sql bağlantısını aç.


            SqlCommand cmd = new SqlCommand("select * from musteri", con);  // yapmak istediğin komutu ver.
            SqlDataReader dr = cmd.ExecuteReader();   // sqlDataReader ile cmd komutu oku.

            while (dr.Read())
            {
                Musteri musteri = new Musteri();

                //musteri.id = int.Parse((string)dr["id"]);             
                //musteri.id = int.Parse(dr["id"].ToString()); 
                musteri.id = Convert.ToInt32(dr["id"]);     // üçüde aynı şey.
                musteri.ad = dr["ad"].ToString();
                musteri.soyad = dr["soyad"].ToString();
                musteri.cinsiyet = (string)dr["cinsiyet"];
                musteri.yas = int.Parse(dr["yas"].ToString());


                musteriList.Add(musteri);
                /*
                 okunmuş olan verileri musteri classındaki proportilerime ekleyerek verileri okudum.
                sonra musteriList esine ekledim.  dr.Read()  döngü bitene kadar bir sonraki kaydı ouyup 
                musteriList  ekleyecek.          
                */
            }

            con.Close();    // verileri aldıktan sonra bağlantıyı kapatıyorum.


            foreach (Musteri musteri in musteriList)
            {
                Console.WriteLine("id: " + musteri.id + "\tad: " + musteri.ad + "\tsoyad: " + musteri.soyad +
                    "\tcinsiyet: " + musteri.cinsiyet + "\tyas: " + musteri.yas);

            }
            //  + yerine virgül(,) koyunca çalışmadı!!! 



            Console.ReadLine();
        }
        public static void LoginTableKayitlariniGetir()
        {

            List<LoginTable> loginTableList = new List<LoginTable>();
            // LoginTable tipinde bir generic class oluşturdum.
            // sqldatareader ile okuduğum değerleri bu liste içinde döndürerek yazıcam.

            con.Open();  // static olarak en başta bağlantıyı yaptım.


            SqlCommand cmd = new SqlCommand("select * from loginTable", con);
            SqlDataReader dr = cmd.ExecuteReader();

            //LoginTable loginTable = new LoginTable();

            while (dr.Read())
            {
                LoginTable loginTable = new LoginTable();

                loginTable.id = Convert.ToInt32(dr["id"].ToString());
                loginTable.kullaniciAdi = dr["kullaniciAdi"].ToString();
                loginTable.sifre = int.Parse(dr["sifre"].ToString());
                loginTable.yetki = dr["yetki"].ToString();
                loginTableList.Add(loginTable);
                /*
                 okunmuş olan verileri LoginTable classındaki proportilerime ekleyerek verileri okudum.
                sonra loginTableList ekledim.  dr.Read()  döngü bitene kadar bir sonraki kaydı okuyup 
                loginTableList ekleyecek.          
                */


            }
            con.Close();

            foreach (LoginTable lt in loginTableList)
            {
                Console.WriteLine("id: " + lt.id + "\tad: " + lt.kullaniciAdi + "\tsifre: " + lt.sifre + "\tyetki: " + lt.yetki);
            }

            Console.ReadLine();
        }
        public static void LoginKayitEkle(string kullaniciAdi, int sifre, string yetki)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("insert into loginTable (kullaniciAdi,sifre,yetki) values(@kullaniciAdi,@sifre,@yetki", con);


            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@sifre", sifre);
            cmd.Parameters.AddWithValue("@yetki", yetki);
            //  aldığım bilgileri parametre olarak values değerlerine atadım. ordan da sql command ile loginTable eklenecek.


            int donenDeger = cmd.ExecuteNonQuery(); // bize bir değer dönecek. değer 0 ise başarısız. 1 ise başarılı.

            if (donenDeger == 0)
            {
                Console.WriteLine("Kayıt ekleme başarısız...");
            }
            else
            {
                Console.WriteLine("Kayıt ekleme başarılı...");
            }



            Console.ReadLine();

            con.Close();



        }

    }


}

