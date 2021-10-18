using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace DataAccess
{
    public partial class LAF_BUPILICContext : DbContext
    {
        public LAF_BUPILICContext(DbContextOptions<LAF_BUPILICContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chtanim> Chtanim { get; set; }
        public virtual DbSet<Chtanim1> Chtanim1 { get; set; }
        public virtual DbSet<ChtanimB2bkullanici> ChtanimB2bkullanici { get; set; }
        public virtual DbSet<ChtanimCh> ChtanimCh { get; set; }
        public virtual DbSet<Fiyatliste> Fiyatliste { get; set; }
        public virtual DbSet<FiyatlisteAlt> FiyatlisteAlt { get; set; }
        public virtual DbSet<FiyatlisteAltsevkadres> FiyatlisteAltsevkadres { get; set; }
        public virtual DbSet<FiyatlisteCari> FiyatlisteCari { get; set; }
        public virtual DbSet<FiyatlisteCarigrup> FiyatlisteCarigrup { get; set; }
        public virtual DbSet<FiyatlisteCarikategori> FiyatlisteCarikategori { get; set; }
        public virtual DbSet<FiyatlisteDetay> FiyatlisteDetay { get; set; }
        public virtual DbSet<Iskontoliste> Iskontoliste { get; set; }
        public virtual DbSet<IskontolisteCari> IskontolisteCari { get; set; }
        public virtual DbSet<IskontolisteDetay> IskontolisteDetay { get; set; }
        public virtual DbSet<Stokozelkod> Stokozelkod { get; set; }
        public virtual DbSet<Stoktanim> Stoktanim { get; set; }
        public virtual DbSet<Stoktanim1> Stoktanim1 { get; set; }
        public virtual DbSet<Wcarikart> Wcarikart { get; set; }
        public virtual DbSet<WebappLoginLog> WebappLoginLog { get; set; }
        public virtual DbSet<WebappSepet> WebappSepet { get; set; }
        public virtual DbSet<Websepet> Websepet { get; set; }
        public virtual DbSet<Websiparis> Websiparis { get; set; }
        public virtual DbSet<Websipariskalem> Websipariskalem { get; set; }
        public virtual DbSet<Wfiyat> Wfiyat { get; set; }
        public virtual DbSet<Wsiparis> Wsiparis { get; set; }
        public virtual DbSet<Wsiparisdetay> Wsiparisdetay { get; set; }
        public virtual DbSet<Wstokbakiye> Wstokbakiye { get; set; }
        public virtual DbSet<Wstokhareket> Wstokhareket { get; set; }
        public virtual DbSet<SiparisDetayFiyatli> SiparisDetayFiyatli { get; set; }
        public virtual DbSet<SiparisDetay> SiparisDetay{ get; set; }
        public virtual DbSet<KampanyaListe> KampanyaListe { get; set; }
        public virtual DbSet<Kampanyalar> Kampanyalar { get; set; }
        public virtual DbSet<FaturaListesi> FaturaListesi { get; set; }
        public virtual DbSet<FiyatListeWeb> FiyatListeWeb { get; set; }
        public virtual DbSet<ChAdres> ChAdres { get; set; }
        public virtual DbSet<İptalSiparisLog> İptalSiparisLog { get; set; }
        public virtual DbSet<Anket> Anket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "led");

            modelBuilder.Entity<Anket>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToView("Anket");
            });

            modelBuilder.Entity<WebappLoginLog>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToView("iptalsiparisLog");
            });

            modelBuilder.Entity<ChAdres>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToView("chtanim_chadres");
            });

            modelBuilder.Entity<FiyatListeWeb>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("wFiyatListeWeb");
            });

            modelBuilder.Entity<FaturaListesi>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToView("wFaturaListesiWeb");
            });

            modelBuilder.Entity<Kampanyalar>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToView("kampanyalar");
            });

            modelBuilder.Entity<KampanyaListe>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToView("kampanyaliste");
            });

            modelBuilder.Entity<SiparisDetayFiyatli>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToView("wSiparisDetayFiyatliWeb");
            });

            modelBuilder.Entity<SiparisDetay>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToView("wSiparisDetayWeb");
            });

            modelBuilder.Entity<Chtanim>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("chtanim", "dbo");

                entity.Property(e => e.B2bkullaniciadi)
                    .HasColumnName("B2BKULLANICIADI")
                    .HasMaxLength(100);

                entity.Property(e => e.B2bparola)
                    .HasColumnName("B2BPAROLA")
                    .HasMaxLength(100);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Unvan)
                    .HasColumnName("CHUNVAN")
                    .HasMaxLength(255);

                entity.Property(e => e.Kod)
                    .HasColumnName("KOD")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Chtanim1>(entity =>
            {
                entity.ToTable("chtanim");

                entity.HasIndex(e => new { e.Id, e.Isim, e.Kod, e.Aktif, e.Tip })
                    .HasName("INDEX_TIP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Aktif).HasColumnName("AKTIF");

                entity.Property(e => e.Dovizcinsi)
                    .HasColumnName("DOVIZCINSI")
                    .HasMaxLength(3);

                entity.Property(e => e.Isim)
                    .HasColumnName("ISIM")
                    .HasMaxLength(255);

                entity.Property(e => e.Kisaisim)
                    .HasColumnName("KISAISIM")
                    .HasMaxLength(150);

                entity.Property(e => e.Kod)
                    .HasColumnName("KOD")
                    .HasMaxLength(100);

                entity.Property(e => e.Led5refid).HasColumnName("LED5REFID");

                entity.Property(e => e.Listesirano)
                    .HasColumnName("LISTESIRANO")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Subeid).HasColumnName("SUBEID");

                entity.Property(e => e.Tip)
                    .HasColumnName("TIP")
                    .HasMaxLength(4);
            });

            modelBuilder.Entity<ChtanimB2bkullanici>(entity =>
            {
                entity.ToTable("chtanim_b2bkullanici");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Anahtar)
                    .HasColumnName("ANAHTAR")
                    .HasMaxLength(100);

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Depoid).HasColumnName("DEPOID");

                entity.Property(e => e.Kullaniciadi)
                    .HasColumnName("KULLANICIADI")
                    .HasMaxLength(100);

                entity.Property(e => e.Parola)
                    .HasColumnName("PAROLA")
                    .HasMaxLength(100);

                entity.Property(e => e.Sevkadresid).HasColumnName("SEVKADRESID");

                entity.Property(e => e.Songiris)
                    .HasColumnName("SONGIRIS")
                    .HasColumnType("datetime");

                entity.Property(e => e.YetkiEkstre).HasColumnName("YETKI_EKSTRE");

                entity.Property(e => e.YetkiSiparis).HasColumnName("YETKI_SIPARIS");

                entity.Property(e => e.YetkiSiparisfiyat).HasColumnName("YETKI_SIPARISFIYAT");
            });

            modelBuilder.Entity<ChtanimCh>(entity =>
            {
                entity.ToTable("chtanim_ch");

                entity.HasIndex(e => e.Chid)
                    .HasName("CHID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Acilistarih)
                    .HasColumnName("ACILISTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ad)
                    .HasColumnName("AD")
                    .HasMaxLength(255);

                entity.Property(e => e.Adres)
                    .HasColumnName("ADRES")
                    .HasMaxLength(255);

                entity.Property(e => e.Alisfiyatlisteid).HasColumnName("ALISFIYATLISTEID");

                entity.Property(e => e.Anachid).HasColumnName("ANACHID");

                entity.Property(e => e.Anachidvar).HasColumnName("ANACHIDVAR");

                entity.Property(e => e.AsistanEposta)
                    .HasColumnName("ASISTAN_EPOSTA")
                    .HasMaxLength(50);

                entity.Property(e => e.B2bkullaniciadi)
                    .HasColumnName("B2BKULLANICIADI")
                    .HasMaxLength(100);

                entity.Property(e => e.B2bparola)
                    .HasColumnName("B2BPAROLA")
                    .HasMaxLength(100);

                entity.Property(e => e.Barkod)
                    .HasColumnName("BARKOD")
                    .HasMaxLength(255);

                entity.Property(e => e.Bilgisayarciktiprofilid).HasColumnName("BILGISAYARCIKTIPROFILID");

                entity.Property(e => e.Carisecimnotu).HasColumnName("CARISECIMNOTU");

                entity.Property(e => e.CarisecimnotuMasaustu).HasColumnName("CARISECIMNOTU_MASAUSTU");

                entity.Property(e => e.CarisecimnotuMobil).HasColumnName("CARISECIMNOTU_MOBIL");

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Deger1)
                    .HasColumnName("DEGER1")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Depoid).HasColumnName("DEPOID");

                entity.Property(e => e.Dijitaltel).HasColumnName("DIJITALTEL");

                entity.Property(e => e.Dovizcinsi)
                    .HasColumnName("DOVIZCINSI")
                    .HasMaxLength(50);

                entity.Property(e => e.EarsivEposta)
                    .HasColumnName("EARSIV_EPOSTA")
                    .HasMaxLength(80);

                entity.Property(e => e.Ebelgekullan)
                    .HasColumnName("EBELGEKULLAN")
                    .HasMaxLength(4);

                entity.Property(e => e.Efaturaetiket)
                    .HasColumnName("EFATURAETIKET")
                    .HasMaxLength(255);

                entity.Property(e => e.Efaturakullaniyor).HasColumnName("EFATURAKULLANIYOR");

                entity.Property(e => e.Eirsaliyeetiket)
                    .HasColumnName("EIRSALIYEETIKET")
                    .HasMaxLength(255);

                entity.Property(e => e.Eirsaliyekullaniyor).HasColumnName("EIRSALIYEKULLANIYOR");

                entity.Property(e => e.Ekalan1)
                    .HasColumnName("EKALAN1")
                    .HasMaxLength(255);

                entity.Property(e => e.Ekalan10)
                    .HasColumnName("EKALAN10")
                    .HasMaxLength(255);

                entity.Property(e => e.Ekalan2)
                    .HasColumnName("EKALAN2")
                    .HasMaxLength(255);

                entity.Property(e => e.Ekalan3)
                    .HasColumnName("EKALAN3")
                    .HasMaxLength(255);

                entity.Property(e => e.Ekalan4)
                    .HasColumnName("EKALAN4")
                    .HasMaxLength(255);

                entity.Property(e => e.Ekalan5)
                    .HasColumnName("EKALAN5")
                    .HasMaxLength(255);

                entity.Property(e => e.Ekalan6)
                    .HasColumnName("EKALAN6")
                    .HasMaxLength(255);

                entity.Property(e => e.Ekalan7)
                    .HasColumnName("EKALAN7")
                    .HasMaxLength(255);

                entity.Property(e => e.Ekalan8)
                    .HasColumnName("EKALAN8")
                    .HasMaxLength(255);

                entity.Property(e => e.Ekalan9)
                    .HasColumnName("EKALAN9")
                    .HasMaxLength(255);

                entity.Property(e => e.EmutabakatEposta)
                    .HasColumnName("EMUTABAKAT_EPOSTA")
                    .HasMaxLength(50);

                entity.Property(e => e.Emutabakatkullaniyor).HasColumnName("EMUTABAKATKULLANIYOR");

                entity.Property(e => e.Entegrasyonkod1)
                    .HasColumnName("ENTEGRASYONKOD1")
                    .HasMaxLength(50);

                entity.Property(e => e.Entegrasyonkod2)
                    .HasColumnName("ENTEGRASYONKOD2")
                    .HasMaxLength(50);

                entity.Property(e => e.Entegrasyonkod3)
                    .HasColumnName("ENTEGRASYONKOD3")
                    .HasMaxLength(50);

                entity.Property(e => e.Eposta)
                    .HasColumnName("EPOSTA")
                    .HasMaxLength(100);

                entity.Property(e => e.Epostaizin).HasColumnName("EPOSTAIZIN");

                entity.Property(e => e.Farklisevkadreskullaniliyor).HasColumnName("FARKLISEVKADRESKULLANILIYOR");

                entity.Property(e => e.Fax)
                    .HasColumnName("FAX")
                    .HasMaxLength(100);

                entity.Property(e => e.Fiyatkriteri).HasColumnName("FIYATKRITERI");

                entity.Property(e => e.Fiyatlisteid).HasColumnName("FIYATLISTEID");

                entity.Property(e => e.Gpsboylam).HasColumnName("GPSBOYLAM");

                entity.Property(e => e.Gpsenlem).HasColumnName("GPSENLEM");

                entity.Property(e => e.Gsm)
                    .HasColumnName("GSM")
                    .HasMaxLength(100);

                entity.Property(e => e.Il)
                    .HasColumnName("IL")
                    .HasMaxLength(50);

                entity.Property(e => e.Ilce)
                    .HasColumnName("ILCE")
                    .HasMaxLength(50);

                entity.Property(e => e.Iskonto1)
                    .HasColumnName("ISKONTO1")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Iskonto2)
                    .HasColumnName("ISKONTO2")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Iskontolisteid).HasColumnName("ISKONTOLISTEID");

                entity.Property(e => e.Kategori10id).HasColumnName("KATEGORI10ID");

                entity.Property(e => e.Kategori1id).HasColumnName("KATEGORI1ID");

                entity.Property(e => e.Kategori2id).HasColumnName("KATEGORI2ID");

                entity.Property(e => e.Kategori3id).HasColumnName("KATEGORI3ID");

                entity.Property(e => e.Kategori4id).HasColumnName("KATEGORI4ID");

                entity.Property(e => e.Kategori5id).HasColumnName("KATEGORI5ID");

                entity.Property(e => e.Kategori6id).HasColumnName("KATEGORI6ID");

                entity.Property(e => e.Kategori7id).HasColumnName("KATEGORI7ID");

                entity.Property(e => e.Kategori8id).HasColumnName("KATEGORI8ID");

                entity.Property(e => e.Kategori9id).HasColumnName("KATEGORI9ID");

                entity.Property(e => e.Kdvdahil).HasColumnName("KDVDAHIL");

                entity.Property(e => e.Kdvkriteri).HasColumnName("KDVKRITERI");

                entity.Property(e => e.Kredilimit)
                    .HasColumnName("KREDILIMIT")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LedmuhMusterihesapid).HasColumnName("LEDMUH_MUSTERIHESAPID");

                entity.Property(e => e.LedmuhSaticihesapid).HasColumnName("LEDMUH_SATICIHESAPID");

                entity.Property(e => e.MobilEvraknoayritakipet).HasColumnName("MOBIL_EVRAKNOAYRITAKIPET");

                entity.Property(e => e.MobilFiyatdegistirme)
                    .HasColumnName("MOBIL_FIYATDEGISTIRME")
                    .HasMaxLength(2);

                entity.Property(e => e.MobilFiyatliirsaliye)
                    .HasColumnName("MOBIL_FIYATLIIRSALIYE")
                    .HasMaxLength(4);

                entity.Property(e => e.MobilGpskonumkisitlama)
                    .HasColumnName("MOBIL_GPSKONUMKISITLAMA")
                    .HasMaxLength(2);

                entity.Property(e => e.MobilIskontofiyatyansitma)
                    .HasColumnName("MOBIL_ISKONTOFIYATYANSITMA")
                    .HasMaxLength(2);

                entity.Property(e => e.MobilKredilimityetki)
                    .HasColumnName("MOBIL_KREDILIMITYETKI")
                    .HasMaxLength(2);

                entity.Property(e => e.MobilPartinotakip).HasColumnName("MOBIL_PARTINOTAKIP");

                entity.Property(e => e.MobilRisklimityetki)
                    .HasColumnName("MOBIL_RISKLIMITYETKI")
                    .HasMaxLength(2);

                entity.Property(e => e.MobilVadelimityetki)
                    .HasColumnName("MOBIL_VADELIMITYETKI")
                    .HasMaxLength(2);

                entity.Property(e => e.Muafiyet).HasColumnName("MUAFIYET");

                entity.Property(e => e.Musterimuhasebekodu)
                    .HasColumnName("MUSTERIMUHASEBEKODU")
                    .HasMaxLength(50);

                entity.Property(e => e.Ozelkod1)
                    .HasColumnName("OZELKOD1")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod2)
                    .HasColumnName("OZELKOD2")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod3)
                    .HasColumnName("OZELKOD3")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod4)
                    .HasColumnName("OZELKOD4")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod5)
                    .HasColumnName("OZELKOD5")
                    .HasMaxLength(100);

                entity.Property(e => e.Renkkodu)
                    .HasColumnName("RENKKODU")
                    .HasMaxLength(6);

                entity.Property(e => e.Resim1)
                    .HasColumnName("RESIM1")
                    .HasColumnType("image");

                entity.Property(e => e.Resim2)
                    .HasColumnName("RESIM2")
                    .HasColumnType("image");

                entity.Property(e => e.Resim3)
                    .HasColumnName("RESIM3")
                    .HasColumnType("image");

                entity.Property(e => e.Resim4)
                    .HasColumnName("RESIM4")
                    .HasColumnType("image");

                entity.Property(e => e.Resim5)
                    .HasColumnName("RESIM5")
                    .HasColumnType("image");

                entity.Property(e => e.Risklimit)
                    .HasColumnName("RISKLIMIT")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Saticimuhasebekodu)
                    .HasColumnName("SATICIMUHASEBEKODU")
                    .HasMaxLength(50);

                entity.Property(e => e.Satisyapma).HasColumnName("SATISYAPMA");

                entity.Property(e => e.SatisyapmaSebep)
                    .HasColumnName("SATISYAPMA_SEBEP")
                    .HasMaxLength(255);

                entity.Property(e => e.Smsizin).HasColumnName("SMSIZIN");

                entity.Property(e => e.Soyad)
                    .HasColumnName("SOYAD")
                    .HasMaxLength(255);

                entity.Property(e => e.Stokiskonto1var).HasColumnName("STOKISKONTO1VAR");

                entity.Property(e => e.Stokiskonto2var).HasColumnName("STOKISKONTO2VAR");

                entity.Property(e => e.Stokiskonto3var).HasColumnName("STOKISKONTO3VAR");

                entity.Property(e => e.Tabelaisim)
                    .HasColumnName("TABELAISIM")
                    .HasMaxLength(255);

                entity.Property(e => e.Tapdkno)
                    .HasColumnName("TAPDKNO")
                    .HasMaxLength(50);

                entity.Property(e => e.Tel1)
                    .HasColumnName("TEL1")
                    .HasMaxLength(100);

                entity.Property(e => e.Tel2)
                    .HasColumnName("TEL2")
                    .HasMaxLength(100);

                entity.Property(e => e.Temsilciid).HasColumnName("TEMSILCIID");

                entity.Property(e => e.TerminalciktiprofilidF2).HasColumnName("TERMINALCIKTIPROFILID_F2");

                entity.Property(e => e.TerminalciktiprofilidFb).HasColumnName("TERMINALCIKTIPROFILID_FB");

                entity.Property(e => e.TerminalciktiprofilidI2).HasColumnName("TERMINALCIKTIPROFILID_I2");

                entity.Property(e => e.TerminalciktiprofilidN2).HasColumnName("TERMINALCIKTIPROFILID_N2");

                entity.Property(e => e.TerminalislemF2).HasColumnName("TERMINALISLEM_F2");

                entity.Property(e => e.TerminalislemFb).HasColumnName("TERMINALISLEM_FB");

                entity.Property(e => e.TerminalislemI2).HasColumnName("TERMINALISLEM_I2");

                entity.Property(e => e.TerminalislemKullan).HasColumnName("TERMINALISLEM_KULLAN");

                entity.Property(e => e.TerminalislemN2).HasColumnName("TERMINALISLEM_N2");

                entity.Property(e => e.TerminalyetkiFiyatdegistirme).HasColumnName("TERMINALYETKI_FIYATDEGISTIRME");

                entity.Property(e => e.TerminalyetkiGpskonum).HasColumnName("TERMINALYETKI_GPSKONUM");

                entity.Property(e => e.Ticarisicilno)
                    .HasColumnName("TICARISICILNO")
                    .HasMaxLength(50);

                entity.Property(e => e.Ulke)
                    .HasColumnName("ULKE")
                    .HasMaxLength(100);

                entity.Property(e => e.Vergidaire)
                    .HasColumnName("VERGIDAIRE")
                    .HasMaxLength(50);

                entity.Property(e => e.Vergino)
                    .HasColumnName("VERGINO")
                    .HasMaxLength(50);

                entity.Property(e => e.Web)
                    .HasColumnName("WEB")
                    .HasMaxLength(50);

                entity.Property(e => e.Yetkili)
                    .HasColumnName("YETKILI")
                    .HasMaxLength(255);

                entity.Property(e => e.Yetkiliadres)
                    .HasColumnName("YETKILIADRES")
                    .HasMaxLength(400);

                entity.Property(e => e.Zorunluparabirimi).HasColumnName("ZORUNLUPARABIRIMI");
            });

            modelBuilder.Entity<Fiyatliste>(entity =>
            {
                entity.ToTable("fiyatliste");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Aktif).HasColumnName("AKTIF");

                entity.Property(e => e.Carisecimtip).HasColumnName("CARISECIMTIP");

                entity.Property(e => e.Isim)
                    .HasColumnName("ISIM")
                    .HasMaxLength(255);

                entity.Property(e => e.Islemtip)
                    .HasColumnName("ISLEMTIP")
                    .HasMaxLength(4);

                entity.Property(e => e.Kdvdahil).HasColumnName("KDVDAHIL");

                entity.Property(e => e.Kod)
                    .HasColumnName("KOD")
                    .HasMaxLength(100);

                entity.Property(e => e.Led5refid).HasColumnName("LED5REFID");

                entity.Property(e => e.Onay1).HasColumnName("ONAY1");

                entity.Property(e => e.Onay1userid).HasColumnName("ONAY1USERID");

                entity.Property(e => e.Onay2).HasColumnName("ONAY2");

                entity.Property(e => e.Onay2userid).HasColumnName("ONAY2USERID");

                entity.Property(e => e.Ozelkod1)
                    .HasColumnName("OZELKOD1")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod2)
                    .HasColumnName("OZELKOD2")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod3)
                    .HasColumnName("OZELKOD3")
                    .HasMaxLength(100);

                entity.Property(e => e.ReferansCarpan)
                    .HasColumnName("REFERANS_CARPAN")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ReferansListeid).HasColumnName("REFERANS_LISTEID");

                entity.Property(e => e.Subeid).HasColumnName("SUBEID");

                entity.Property(e => e.Tarih)
                    .HasColumnName("TARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tip)
                    .HasColumnName("TIP")
                    .HasMaxLength(4);
            });

            modelBuilder.Entity<FiyatlisteAlt>(entity =>
            {
                entity.ToTable("fiyatliste_alt");

                entity.HasIndex(e => new { e.Aciklama, e.Aktif, e.Id, e.Sontarihvar, e.Ilktarihvar, e.Kdvdahil, e.Led5refid, e.Onay1, e.Onay1userid, e.Onay2, e.Onay2userid, e.Sevkadressecimtip, e.Fiyatlisteid, e.Ilktarih, e.Sontarih })
                    .HasName("INDEX_FIYATLISTEID_ILKSONTARIH");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Aciklama)
                    .HasColumnName("ACIKLAMA")
                    .HasMaxLength(255);

                entity.Property(e => e.Aktif).HasColumnName("AKTIF");

                entity.Property(e => e.Fiyatlisteid).HasColumnName("FIYATLISTEID");

                entity.Property(e => e.Ilktarih)
                    .HasColumnName("ILKTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ilktarihvar).HasColumnName("ILKTARIHVAR");

                entity.Property(e => e.Kdvdahil).HasColumnName("KDVDAHIL");

                entity.Property(e => e.Led5refid).HasColumnName("LED5REFID");

                entity.Property(e => e.Onay1).HasColumnName("ONAY1");

                entity.Property(e => e.Onay1userid).HasColumnName("ONAY1USERID");

                entity.Property(e => e.Onay2).HasColumnName("ONAY2");

                entity.Property(e => e.Onay2userid).HasColumnName("ONAY2USERID");

                entity.Property(e => e.Sevkadressecimtip).HasColumnName("SEVKADRESSECIMTIP");

                entity.Property(e => e.Sontarih)
                    .HasColumnName("SONTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Sontarihvar).HasColumnName("SONTARIHVAR");
            });

            modelBuilder.Entity<FiyatlisteAltsevkadres>(entity =>
            {
                entity.ToTable("fiyatliste_altsevkadres");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fiyataltlisteid).HasColumnName("FIYATALTLISTEID");

                entity.Property(e => e.Sevkadresid).HasColumnName("SEVKADRESID");
            });

            modelBuilder.Entity<FiyatlisteCari>(entity =>
            {
                entity.ToTable("fiyatliste_cari");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Fiyatlisteid).HasColumnName("FIYATLISTEID");
            });

            modelBuilder.Entity<FiyatlisteCarigrup>(entity =>
            {
                entity.ToTable("fiyatliste_carigrup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fiyatlisteid).HasColumnName("FIYATLISTEID");

                entity.Property(e => e.Grupid).HasColumnName("GRUPID");
            });

            modelBuilder.Entity<FiyatlisteCarikategori>(entity =>
            {
                entity.ToTable("fiyatliste_carikategori");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fiyatlisteid).HasColumnName("FIYATLISTEID");

                entity.Property(e => e.Kategoriid).HasColumnName("KATEGORIID");

                entity.Property(e => e.Kategoritip)
                    .HasColumnName("KATEGORITIP")
                    .HasMaxLength(4);
            });

            modelBuilder.Entity<FiyatlisteDetay>(entity =>
            {
                entity.ToTable("fiyatliste_detay");

                entity.HasIndex(e => new { e.Fiyatlisteid, e.Iskonto1, e.Birimid, e.Id, e.Fiyataltlisteid, e.Fiyat, e.Duzenlemetarih, e.Oncekifiyat, e.Iskonto3, e.Iskonto2, e.Stokid })
                    .HasName("INDEX_STOKID");

                entity.HasIndex(e => new { e.Stokid, e.Birimid, e.Duzenlemetarih, e.Fiyat, e.Fiyatlisteid, e.Id, e.Iskonto1, e.Iskonto2, e.Iskonto3, e.Oncekifiyat, e.Fiyataltlisteid })
                    .HasName("INDEX_ALTLISTEID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birimid).HasColumnName("BIRIMID");

                entity.Property(e => e.Duzenlemetarih)
                    .HasColumnName("DUZENLEMETARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fiyat)
                    .HasColumnName("FIYAT")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fiyataltlisteid).HasColumnName("FIYATALTLISTEID");

                entity.Property(e => e.Fiyatlisteid).HasColumnName("FIYATLISTEID");

                entity.Property(e => e.Iskonto1)
                    .HasColumnName("ISKONTO1")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iskonto2)
                    .HasColumnName("ISKONTO2")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iskonto3)
                    .HasColumnName("ISKONTO3")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Oncekifiyat)
                    .HasColumnName("ONCEKIFIYAT")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Stokid).HasColumnName("STOKID");
            });

            modelBuilder.Entity<Iskontoliste>(entity =>
            {
                entity.ToTable("iskontoliste");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<IskontolisteCari>(entity =>
            {
                entity.ToTable("iskontoliste_cari");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Iskontolisteid).HasColumnName("ISKONTOLISTEID");
            });

            modelBuilder.Entity<IskontolisteDetay>(entity =>
            {
                entity.ToTable("iskontoliste_detay");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Stokozelkod>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("stokozelkod", "dbo");

                entity.Property(e => e.Aciklama)
                    .HasColumnName("ACIKLAMA")
                    .HasMaxLength(255);

                entity.Property(e => e.Kisaisim)
                    .HasColumnName("KISAISIM")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkodno).HasColumnName("OZELKODNO");
            });

            modelBuilder.Entity<Stoktanim>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("stoktanim", "dbo");

                entity.Property(e => e.Aktif)
                    .IsRequired()
                    .HasColumnName("AKTIF")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ambalajbirim)
                    .HasColumnName("AMBALAJBIRIM")
                    .HasMaxLength(100);

                entity.Property(e => e.Barkod1)
                    .HasColumnName("BARKOD1")
                    .HasMaxLength(100);

                entity.Property(e => e.Barkod2)
                    .HasColumnName("BARKOD2")
                    .HasMaxLength(100);

                entity.Property(e => e.Birim)
                    .HasColumnName("BIRIM")
                    .HasMaxLength(100);

                entity.Property(e => e.Entegrasyonkod)
                    .HasColumnName("ENTEGRASYONKOD")
                    .HasMaxLength(255);

                entity.Property(e => e.ListeFiyat)
                    .HasColumnName("LISTEFIYAT")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ozelkod1)
                    .IsRequired()
                    .HasColumnName("OZELKOD1")
                    .HasMaxLength(255);

                entity.Property(e => e.Ozelkod2)
                    .IsRequired()
                    .HasColumnName("OZELKOD2")
                    .HasMaxLength(255);

                entity.Property(e => e.Ozelkod3)
                    .IsRequired()
                    .HasColumnName("OZELKOD3")
                    .HasMaxLength(255);

                entity.Property(e => e.Ozelkod4)
                    .IsRequired()
                    .HasColumnName("OZELKOD4")
                    .HasMaxLength(255);

                entity.Property(e => e.Ozelkod5)
                    .IsRequired()
                    .HasColumnName("OZELKOD5")
                    .HasMaxLength(255);

                entity.Property(e => e.Siparislistevar).HasColumnName("SIPARISLISTEVAR");

                entity.Property(e => e.Stokisim)
                    .HasColumnName("STOKISIM")
                    .HasMaxLength(255);

                entity.Property(e => e.Stokkod)
                    .HasColumnName("STOKKOD")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Stoktanim1>(entity =>
            {
                entity.ToTable("stoktanim");

                entity.HasIndex(e => new { e.Urunlistesira, e.Vadekullaniliyor, e.Varyasyonvar, e.Aciklama, e.Aktif, e.Ambalajvar, e.Anastokid, e.Anastokvar, e.Birimalan, e.BirimalanVar, e.Birimalanbirim, e.Birimbrutgramaj, e.BirimbrutgramajVar, e.Birimbrutgramajbirim, e.BirimeknitelikVar, e.Birimeknitelikbirim, e.Birimeknitelikbolen, e.Birimeknitelikcarpan, e.Birimgramaj, e.BirimgramajVar, e.Birimgramajbirim, e.Birimhacim, e.BirimhacimVar, e.Birimhacimbirim, e.Birimid, e.Birimsivihacim, e.BirimsivihacimVar, e.Birimsivihacimbirim, e.Birimuzunluk, e.BirimuzunlukVar, e.Birimuzunlukbirim, e.Dovizcinsi, e.Entegrasyonkod, e.Fiyat1, e.Fiyat2, e.Fiyat3, e.Fiyat4, e.Fiyat5, e.Fiyatalis, e.Fiyatperakende, e.Id, e.Isim, e.Iskonto1, e.Iskonto2, e.Iskonto3, e.Kategori1id, e.Kategori2id, e.Kategori3id, e.Kategori4id, e.Kategori5id, e.Kdvyuzde, e.Kdvyuzde2, e.Kdvyuzde3, e.Kisaisim, e.Kod, e.Kritikstok, e.Maxfiyat, e.Maxisk1, e.Maxisk2, e.Maxisk3, e.Maxstok, e.Minfiyat, e.Minisk1, e.Minisk2, e.Minisk3, e.MuhasebeAlisiade, e.MuhasebeAliskod, e.MuhasebeIndirilenkdv, e.MuhasebeKod1, e.MuhasebeKod2, e.MuhasebeSatisiade, e.MuhasebeSatiskdv, e.MuhasebeSatiskod, e.Otvkullaniliyor, e.Otvyuzde, e.Ozelkod1, e.Ozelkod2, e.Ozelkod3, e.Ozelkod4, e.Ozelkod5, e.Ozelmatrahfiyat, e.Ozelmatrahvar, e.Partinovar, e.Programkod, e.Serinovar, e.Stokvadegun, e.Stokvadekullaniliyor, e.Stokvadetip, e.Tevkifatyuzde, e.Tip })
                    .HasName("INDEX_TIP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Aciklama)
                    .HasColumnName("ACIKLAMA")
                    .HasMaxLength(255);

                entity.Property(e => e.Aktif).HasColumnName("AKTIF");

                entity.Property(e => e.Ambalajnitelik)
                    .HasColumnName("AMBALAJNITELIK")
                    .HasMaxLength(50);

                entity.Property(e => e.Ambalajnitelik2)
                    .HasColumnName("AMBALAJNITELIK2")
                    .HasMaxLength(50);

                entity.Property(e => e.Ambalajvar).HasColumnName("AMBALAJVAR");

                entity.Property(e => e.Anastokid).HasColumnName("ANASTOKID");

                entity.Property(e => e.Anastokvar).HasColumnName("ANASTOKVAR");

                entity.Property(e => e.Birimalan)
                    .HasColumnName("BIRIMALAN")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BirimalanVar).HasColumnName("BIRIMALAN_VAR");

                entity.Property(e => e.Birimalanbirim).HasColumnName("BIRIMALANBIRIM");

                entity.Property(e => e.Birimbrutgramaj)
                    .HasColumnName("BIRIMBRUTGRAMAJ")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BirimbrutgramajVar).HasColumnName("BIRIMBRUTGRAMAJ_VAR");

                entity.Property(e => e.Birimbrutgramajbirim).HasColumnName("BIRIMBRUTGRAMAJBIRIM");

                entity.Property(e => e.BirimeknitelikVar).HasColumnName("BIRIMEKNITELIK_VAR");

                entity.Property(e => e.Birimeknitelikbirim)
                    .HasColumnName("BIRIMEKNITELIKBIRIM")
                    .HasMaxLength(50);

                entity.Property(e => e.Birimeknitelikbolen)
                    .HasColumnName("BIRIMEKNITELIKBOLEN")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Birimeknitelikcarpan)
                    .HasColumnName("BIRIMEKNITELIKCARPAN")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Birimgramaj)
                    .HasColumnName("BIRIMGRAMAJ")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BirimgramajVar).HasColumnName("BIRIMGRAMAJ_VAR");

                entity.Property(e => e.Birimgramajbirim).HasColumnName("BIRIMGRAMAJBIRIM");

                entity.Property(e => e.Birimhacim)
                    .HasColumnName("BIRIMHACIM")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BirimhacimVar).HasColumnName("BIRIMHACIM_VAR");

                entity.Property(e => e.Birimhacimbirim).HasColumnName("BIRIMHACIMBIRIM");

                entity.Property(e => e.Birimid).HasColumnName("BIRIMID");

                entity.Property(e => e.BirimidDepohar).HasColumnName("BIRIMID_DEPOHAR");

                entity.Property(e => e.BirimidF1).HasColumnName("BIRIMID_F1");

                entity.Property(e => e.BirimidF2).HasColumnName("BIRIMID_F2");

                entity.Property(e => e.BirimidFiyatliste).HasColumnName("BIRIMID_FIYATLISTE");

                entity.Property(e => e.BirimidS1).HasColumnName("BIRIMID_S1");

                entity.Property(e => e.BirimidS2).HasColumnName("BIRIMID_S2");

                entity.Property(e => e.BirimidU1).HasColumnName("BIRIMID_U1");

                entity.Property(e => e.BirimidU2).HasColumnName("BIRIMID_U2");

                entity.Property(e => e.Birimsivihacim)
                    .HasColumnName("BIRIMSIVIHACIM")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BirimsivihacimVar).HasColumnName("BIRIMSIVIHACIM_VAR");

                entity.Property(e => e.Birimsivihacimbirim).HasColumnName("BIRIMSIVIHACIMBIRIM");

                entity.Property(e => e.Birimuzunluk)
                    .HasColumnName("BIRIMUZUNLUK")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BirimuzunlukVar).HasColumnName("BIRIMUZUNLUK_VAR");

                entity.Property(e => e.Birimuzunlukbirim).HasColumnName("BIRIMUZUNLUKBIRIM");

                entity.Property(e => e.Dokmenaylon)
                    .HasColumnName("DOKMENAYLON")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Dovizcinsi)
                    .HasColumnName("DOVIZCINSI")
                    .HasMaxLength(3);

                entity.Property(e => e.Entegrasyonkod)
                    .HasColumnName("ENTEGRASYONKOD")
                    .HasMaxLength(255);

                entity.Property(e => e.Esik12)
                    .HasColumnName("ESIK12")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Esik6)
                    .HasColumnName("ESIK6")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Etiketagirlik)
                    .HasColumnName("ETIKETAGIRLIK")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Etiketnitelik)
                    .HasColumnName("ETIKETNITELIK")
                    .HasMaxLength(50);

                entity.Property(e => e.Fiyat1)
                    .HasColumnName("FIYAT1")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fiyat2)
                    .HasColumnName("FIYAT2")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fiyat3)
                    .HasColumnName("FIYAT3")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fiyat4)
                    .HasColumnName("FIYAT4")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fiyat5)
                    .HasColumnName("FIYAT5")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fiyatalis)
                    .HasColumnName("FIYATALIS")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fiyatperakende)
                    .HasColumnName("FIYATPERAKENDE")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Gtipno)
                    .HasColumnName("GTIPNO")
                    .HasMaxLength(100);

                entity.Property(e => e.Isim)
                    .HasColumnName("ISIM")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(N'')")
                    .HasComment("Stok Kartı İsmi");

                entity.Property(e => e.Iskonto1)
                    .HasColumnName("ISKONTO1")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iskonto2)
                    .HasColumnName("ISKONTO2")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iskonto3)
                    .HasColumnName("ISKONTO3")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Kaplama)
                    .HasColumnName("KAPLAMA")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Karkasmiktarkg)
                    .HasColumnName("KARKASMIKTARKG")
                    .HasColumnType("numeric(18, 3)");

                entity.Property(e => e.Kategori10id).HasColumnName("KATEGORI10ID");

                entity.Property(e => e.Kategori1id).HasColumnName("KATEGORI1ID");

                entity.Property(e => e.Kategori2id).HasColumnName("KATEGORI2ID");

                entity.Property(e => e.Kategori3id).HasColumnName("KATEGORI3ID");

                entity.Property(e => e.Kategori4id).HasColumnName("KATEGORI4ID");

                entity.Property(e => e.Kategori5id).HasColumnName("KATEGORI5ID");

                entity.Property(e => e.Kategori6id).HasColumnName("KATEGORI6ID");

                entity.Property(e => e.Kategori7id).HasColumnName("KATEGORI7ID");

                entity.Property(e => e.Kategori8id).HasColumnName("KATEGORI8ID");

                entity.Property(e => e.Kategori9id).HasColumnName("KATEGORI9ID");

                entity.Property(e => e.Kategoriagacid).HasColumnName("KATEGORIAGACID");

                entity.Property(e => e.Kdvyuzde)
                    .HasColumnName("KDVYUZDE")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Kdvyuzde2)
                    .HasColumnName("KDVYUZDE2")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Kdvyuzde3)
                    .HasColumnName("KDVYUZDE3")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Kgbasiurtmaliyet)
                    .HasColumnName("KGBASIURTMALIYET")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Kiciadet)
                    .HasColumnName("KICIADET")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Kiciparcaadet)
                    .HasColumnName("KICIPARCAADET")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Kisaisim)
                    .HasColumnName("KISAISIM")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Klipsagirlik)
                    .HasColumnName("KLIPSAGIRLIK")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Kod)
                    .HasColumnName("KOD")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("(N'')")
                    .HasComment("Stok Kartı Kodu");

                entity.Property(e => e.Kolibasiurtmaliyet)
                    .HasColumnName("KOLIBASIURTMALIYET")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Kolitipi)
                    .HasColumnName("KOLITIPI")
                    .HasMaxLength(50);

                entity.Property(e => e.Kritikstok)
                    .HasColumnName("KRITIKSTOK")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Led5refid).HasColumnName("LED5REFID");

                entity.Property(e => e.LedmuhAlisfiyatfarkihesapid).HasColumnName("LEDMUH_ALISFIYATFARKIHESAPID");

                entity.Property(e => e.LedmuhAlisfiyatfarkikdvhesapid).HasColumnName("LEDMUH_ALISFIYATFARKIKDVHESAPID");

                entity.Property(e => e.LedmuhAlishesapid).HasColumnName("LEDMUH_ALISHESAPID");

                entity.Property(e => e.LedmuhAlisiadehesapid).HasColumnName("LEDMUH_ALISIADEHESAPID");

                entity.Property(e => e.LedmuhAlisiadekdvhesapid).HasColumnName("LEDMUH_ALISIADEKDVHESAPID");

                entity.Property(e => e.LedmuhDigerkdvhesapid).HasColumnName("LEDMUH_DIGERKDVHESAPID");

                entity.Property(e => e.LedmuhIhracatsatishesapid).HasColumnName("LEDMUH_IHRACATSATISHESAPID");

                entity.Property(e => e.LedmuhIndirilenkdvhesapid).HasColumnName("LEDMUH_INDIRILENKDVHESAPID");

                entity.Property(e => e.LedmuhSatisfiyatfarkihesapid).HasColumnName("LEDMUH_SATISFIYATFARKIHESAPID");

                entity.Property(e => e.LedmuhSatisfiyatfarkikdvhesapid).HasColumnName("LEDMUH_SATISFIYATFARKIKDVHESAPID");

                entity.Property(e => e.LedmuhSatishesapid).HasColumnName("LEDMUH_SATISHESAPID");

                entity.Property(e => e.LedmuhSatisiadehesapid).HasColumnName("LEDMUH_SATISIADEHESAPID");

                entity.Property(e => e.LedmuhSatisiadekdvhesapid).HasColumnName("LEDMUH_SATISIADEKDVHESAPID");

                entity.Property(e => e.LedmuhSatiskdvhesapid).HasColumnName("LEDMUH_SATISKDVHESAPID");

                entity.Property(e => e.LedmuhYurtdisialishesapid).HasColumnName("LEDMUH_YURTDISIALISHESAPID");

                entity.Property(e => e.LedmuhYurtdisiindirilenkdvhesapid).HasColumnName("LEDMUH_YURTDISIINDIRILENKDVHESAPID");

                entity.Property(e => e.LedmuhYurtdisisatishesapid).HasColumnName("LEDMUH_YURTDISISATISHESAPID");

                entity.Property(e => e.LedmuhYurtdisisatiskdvhesapid).HasColumnName("LEDMUH_YURTDISISATISKDVHESAPID");

                entity.Property(e => e.Listesirano)
                    .HasColumnName("LISTESIRANO")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Maxfiyat)
                    .HasColumnName("MAXFIYAT")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Maxisk1)
                    .HasColumnName("MAXISK1")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Maxisk2)
                    .HasColumnName("MAXISK2")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Maxisk3)
                    .HasColumnName("MAXISK3")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Maxstok)
                    .HasColumnName("MAXSTOK")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Mensei)
                    .HasColumnName("MENSEI")
                    .HasMaxLength(100);

                entity.Property(e => e.Minfiyat)
                    .HasColumnName("MINFIYAT")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Minisk1)
                    .HasColumnName("MINISK1")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Minisk2)
                    .HasColumnName("MINISK2")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Minisk3)
                    .HasColumnName("MINISK3")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.MuhasebeAlisfiyatfarki)
                    .HasColumnName("MUHASEBE_ALISFIYATFARKI")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeAlisfiyatfarkikdv)
                    .HasColumnName("MUHASEBE_ALISFIYATFARKIKDV")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeAlisiade)
                    .HasColumnName("MUHASEBE_ALISIADE")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeAliskod)
                    .HasColumnName("MUHASEBE_ALISKOD")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeIhracatsatiskod)
                    .HasColumnName("MUHASEBE_IHRACATSATISKOD")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeIndirilenkdv)
                    .HasColumnName("MUHASEBE_INDIRILENKDV")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeIskontokod)
                    .HasColumnName("MUHASEBE_ISKONTOKOD")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeIthalataliskdv)
                    .HasColumnName("MUHASEBE_ITHALATALISKDV")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeIthalataliskod)
                    .HasColumnName("MUHASEBE_ITHALATALISKOD")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeKod1)
                    .HasColumnName("MUHASEBE_KOD1")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeKod2)
                    .HasColumnName("MUHASEBE_KOD2")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeSatisfiyatfarki)
                    .HasColumnName("MUHASEBE_SATISFIYATFARKI")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeSatisfiyatfarkikdv)
                    .HasColumnName("MUHASEBE_SATISFIYATFARKIKDV")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeSatisiade)
                    .HasColumnName("MUHASEBE_SATISIADE")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeSatiskdv)
                    .HasColumnName("MUHASEBE_SATISKDV")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeSatiskod)
                    .HasColumnName("MUHASEBE_SATISKOD")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeYurtdisialiskod)
                    .HasColumnName("MUHASEBE_YURTDISIALISKOD")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeYurtdisiindirilenkdv)
                    .HasColumnName("MUHASEBE_YURTDISIINDIRILENKDV")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeYurtdisisatiskdv)
                    .HasColumnName("MUHASEBE_YURTDISISATISKDV")
                    .HasMaxLength(100);

                entity.Property(e => e.MuhasebeYurtdisisatiskod)
                    .HasColumnName("MUHASEBE_YURTDISISATISKOD")
                    .HasMaxLength(100);

                entity.Property(e => e.Opbolen)
                    .HasColumnName("OPBOLEN")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Opcarpan)
                    .HasColumnName("OPCARPAN")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Opparam1)
                    .HasColumnName("OPPARAM1")
                    .HasMaxLength(100);

                entity.Property(e => e.Opparam2)
                    .HasColumnName("OPPARAM2")
                    .HasMaxLength(100);

                entity.Property(e => e.Optimizasyongrubu)
                    .HasColumnName("OPTIMIZASYONGRUBU")
                    .HasMaxLength(150);

                entity.Property(e => e.Ortalamakolikg)
                    .HasColumnName("ORTALAMAKOLIKG")
                    .HasColumnType("numeric(18, 3)");

                entity.Property(e => e.Ortalamapargr)
                    .HasColumnName("ORTALAMAPARGR")
                    .HasColumnType("numeric(18, 3)");

                entity.Property(e => e.Otvkullaniliyor).HasColumnName("OTVKULLANILIYOR");

                entity.Property(e => e.Otvyuzde)
                    .HasColumnName("OTVYUZDE")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Ozelkod1)
                    .HasColumnName("OZELKOD1")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod10)
                    .HasColumnName("OZELKOD10")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod11)
                    .HasColumnName("OZELKOD11")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod12)
                    .HasColumnName("OZELKOD12")
                    .HasMaxLength(50);

                entity.Property(e => e.Ozelkod13)
                    .HasColumnName("OZELKOD13")
                    .HasMaxLength(50);

                entity.Property(e => e.Ozelkod14)
                    .HasColumnName("OZELKOD14")
                    .HasMaxLength(50);

                entity.Property(e => e.Ozelkod15)
                    .HasColumnName("OZELKOD15")
                    .HasMaxLength(50);

                entity.Property(e => e.Ozelkod16)
                    .HasColumnName("OZELKOD16")
                    .HasMaxLength(250);

                entity.Property(e => e.Ozelkod17)
                    .HasColumnName("OZELKOD17")
                    .HasMaxLength(250);

                entity.Property(e => e.Ozelkod2)
                    .HasColumnName("OZELKOD2")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod3)
                    .HasColumnName("OZELKOD3")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod4)
                    .HasColumnName("OZELKOD4")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod5)
                    .HasColumnName("OZELKOD5")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelmatrahfiyat)
                    .HasColumnName("OZELMATRAHFIYAT")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Ozelmatrahvar).HasColumnName("OZELMATRAHVAR");

                entity.Property(e => e.Parcagrubu)
                    .HasColumnName("PARCAGRUBU")
                    .HasMaxLength(250);

                entity.Property(e => e.Partinovar).HasColumnName("PARTINOVAR");

                entity.Property(e => e.PerakendeDepartmanid).HasColumnName("PERAKENDE_DEPARTMANID");

                entity.Property(e => e.PerakendeHedefkarmarji)
                    .HasColumnName("PERAKENDE_HEDEFKARMARJI")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PerakendeKdvyuzde1)
                    .HasColumnName("PERAKENDE_KDVYUZDE")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PerakendeKullan).HasColumnName("PERAKENDE_KULLAN");

                entity.Property(e => e.PerakendeMaxiskonto)
                    .HasColumnName("PERAKENDE_MAXISKONTO")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PerakendeOzelkdv).HasColumnName("PERAKENDE_OZELKDV");

                entity.Property(e => e.PerakendePluno).HasColumnName("PERAKENDE_PLUNO");

                entity.Property(e => e.PerakendeSonkullanmagun).HasColumnName("PERAKENDE_SONKULLANMAGUN");

                entity.Property(e => e.PerakendeTerazigonder).HasColumnName("PERAKENDE_TERAZIGONDER");

                entity.Property(e => e.PerakendeUrunlisteGoster).HasColumnName("PERAKENDE_URUNLISTE_GOSTER");

                entity.Property(e => e.PerakendeUrunlisteGrup)
                    .HasColumnName("PERAKENDE_URUNLISTE_GRUP")
                    .HasMaxLength(255);

                entity.Property(e => e.PerakendeUrunlisteSira).HasColumnName("PERAKENDE_URUNLISTE_SIRA");

                entity.Property(e => e.PerakendeUrunlisteTerazikullan)
                    .HasColumnName("PERAKENDE_URUNLISTE_TERAZIKULLAN")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PerakendeVeresiyeiskonto)
                    .HasColumnName("PERAKENDE_VERESIYEISKONTO")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.PerakendeVeresiyeiskontotip)
                    .HasColumnName("PERAKENDE_VERESIYEISKONTOTIP")
                    .HasMaxLength(4);

                entity.Property(e => e.Perakendekdvyuzde)
                    .HasColumnName("PERAKENDEKDVYUZDE")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PerakendesiparisKullan).HasColumnName("PERAKENDESIPARIS_KULLAN");

                entity.Property(e => e.Posetagirlik)
                    .HasColumnName("POSETAGIRLIK")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Programkod)
                    .HasColumnName("PROGRAMKOD")
                    .HasMaxLength(127);

                entity.Property(e => e.Rafomru).HasColumnName("RAFOMRU");

                entity.Property(e => e.Randimangrup)
                    .HasColumnName("RANDIMANGRUP")
                    .HasMaxLength(250);

                entity.Property(e => e.Randimangrup2)
                    .HasColumnName("RANDIMANGRUP2")
                    .HasMaxLength(250);

                entity.Property(e => e.Randimangrup3)
                    .HasColumnName("RANDIMANGRUP3")
                    .HasMaxLength(250);

                entity.Property(e => e.Receteid).HasColumnName("RECETEID");

                entity.Property(e => e.Resim)
                    .HasColumnName("RESIM")
                    .HasColumnType("image");

                entity.Property(e => e.Resim2)
                    .HasColumnName("RESIM2")
                    .HasColumnType("image");

                entity.Property(e => e.Resim3)
                    .HasColumnName("RESIM3")
                    .HasColumnType("image");

                entity.Property(e => e.Resim4)
                    .HasColumnName("RESIM4")
                    .HasColumnType("image");

                entity.Property(e => e.Serinovar).HasColumnName("SERINOVAR");

                entity.Property(e => e.Siparislistevar).HasColumnName("SIPARISLISTEVAR");

                entity.Property(e => e.Stickeragirlik)
                    .HasColumnName("STICKERAGIRLIK")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Stokvadegun).HasColumnName("STOKVADEGUN");

                entity.Property(e => e.Stokvadekullaniliyor).HasColumnName("STOKVADEKULLANILIYOR");

                entity.Property(e => e.Stokvadetip).HasColumnName("STOKVADETIP");

                entity.Property(e => e.Strecagirlik)
                    .HasColumnName("STRECAGIRLIK")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Strecuzunluk)
                    .HasColumnName("STRECUZUNLUK")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tabakagirlik)
                    .HasColumnName("TABAKAGIRLIK")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Tevkifatyuzde)
                    .HasColumnName("TEVKIFATYUZDE")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tip).HasColumnName("TIP");

                entity.Property(e => e.Uretimhatti)
                    .HasColumnName("URETIMHATTI")
                    .HasMaxLength(250);

                entity.Property(e => e.Urunlistesira).HasColumnName("URUNLISTESIRA");

                entity.Property(e => e.Urunoran)
                    .HasColumnName("URUNORAN")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Uruntip)
                    .HasColumnName("URUNTIP")
                    .HasMaxLength(4);

                entity.Property(e => e.Usturun)
                    .HasColumnName("USTURUN")
                    .HasMaxLength(250);

                entity.Property(e => e.Usturunoran)
                    .HasColumnName("USTURUNORAN")
                    .HasMaxLength(250);

                entity.Property(e => e.Vadekullaniliyor).HasColumnName("VADEKULLANILIYOR");

                entity.Property(e => e.Varyasyonvar).HasColumnName("VARYASYONVAR");
            });

            modelBuilder.Entity<Wcarikart>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("wcarikart");

                entity.Property(e => e.Acilistarih)
                    .HasColumnName("ACILISTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ad)
                    .HasColumnName("AD")
                    .HasMaxLength(255);

                entity.Property(e => e.Adres)
                    .HasColumnName("ADRES")
                    .HasMaxLength(255);

                entity.Property(e => e.Aktif).HasColumnName("AKTIF");

                entity.Property(e => e.Alisfiyatlisteid).HasColumnName("ALISFIYATLISTEID");

                entity.Property(e => e.AsistanEposta)
                    .HasColumnName("ASISTAN_EPOSTA")
                    .HasMaxLength(50);

                entity.Property(e => e.B2bkullaniciadi)
                    .HasColumnName("B2BKULLANICIADI")
                    .HasMaxLength(100);

                entity.Property(e => e.B2bparola)
                    .HasColumnName("B2BPAROLA")
                    .HasMaxLength(100);

                entity.Property(e => e.Barkod)
                    .HasColumnName("BARKOD")
                    .HasMaxLength(255);

                entity.Property(e => e.Carisecimnotu).HasColumnName("CARISECIMNOTU");

                entity.Property(e => e.CarisecimnotuMasaustu).HasColumnName("CARISECIMNOTU_MASAUSTU");

                entity.Property(e => e.CarisecimnotuMobil).HasColumnName("CARISECIMNOTU_MOBIL");

                entity.Property(e => e.Depoid).HasColumnName("DEPOID");

                entity.Property(e => e.Detayid).HasColumnName("DETAYID");

                entity.Property(e => e.Dijitaltel).HasColumnName("DIJITALTEL");

                entity.Property(e => e.Dovizcinsi)
                    .HasColumnName("DOVIZCINSI")
                    .HasMaxLength(3);

                entity.Property(e => e.EarsivEposta)
                    .HasColumnName("EARSIV_EPOSTA")
                    .HasMaxLength(80);

                entity.Property(e => e.Ebelgekullan)
                    .IsRequired()
                    .HasColumnName("EBELGEKULLAN")
                    .HasMaxLength(4);

                entity.Property(e => e.Efaturakullaniyor).HasColumnName("EFATURAKULLANIYOR");

                entity.Property(e => e.Ekalan1)
                    .HasColumnName("EKALAN1")
                    .HasMaxLength(255);

                entity.Property(e => e.EmutabakatEposta)
                    .HasColumnName("EMUTABAKAT_EPOSTA")
                    .HasMaxLength(50);

                entity.Property(e => e.Entegrasyonkod1)
                    .HasColumnName("ENTEGRASYONKOD1")
                    .HasMaxLength(50);

                entity.Property(e => e.Entegrasyonkod2)
                    .HasColumnName("ENTEGRASYONKOD2")
                    .HasMaxLength(50);

                entity.Property(e => e.Entegrasyonkod3)
                    .HasColumnName("ENTEGRASYONKOD3")
                    .HasMaxLength(50);

                entity.Property(e => e.Eposta)
                    .HasColumnName("EPOSTA")
                    .HasMaxLength(100);

                entity.Property(e => e.Epostaizin).HasColumnName("EPOSTAIZIN");

                entity.Property(e => e.Fax)
                    .HasColumnName("FAX")
                    .HasMaxLength(100);

                entity.Property(e => e.Fiyatkriteri).HasColumnName("FIYATKRITERI");

                entity.Property(e => e.Fiyatlisteid).HasColumnName("FIYATLISTEID");

                entity.Property(e => e.Gpsboylam).HasColumnName("GPSBOYLAM");

                entity.Property(e => e.Gpsenlem).HasColumnName("GPSENLEM");

                entity.Property(e => e.Gsm)
                    .HasColumnName("GSM")
                    .HasMaxLength(100);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Il)
                    .HasColumnName("IL")
                    .HasMaxLength(50);

                entity.Property(e => e.Ilce)
                    .HasColumnName("ILCE")
                    .HasMaxLength(50);

                entity.Property(e => e.Isim)
                    .HasColumnName("ISIM")
                    .HasMaxLength(255);

                entity.Property(e => e.Iskonto1)
                    .HasColumnName("ISKONTO1")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Iskonto2)
                    .HasColumnName("ISKONTO2")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Iskontolisteid).HasColumnName("ISKONTOLISTEID");

                entity.Property(e => e.Kategori10id).HasColumnName("KATEGORI10ID");

                entity.Property(e => e.Kategori1id).HasColumnName("KATEGORI1ID");

                entity.Property(e => e.Kategori2id).HasColumnName("KATEGORI2ID");

                entity.Property(e => e.Kategori3id).HasColumnName("KATEGORI3ID");

                entity.Property(e => e.Kategori4id).HasColumnName("KATEGORI4ID");

                entity.Property(e => e.Kategori5id).HasColumnName("KATEGORI5ID");

                entity.Property(e => e.Kategori6id).HasColumnName("KATEGORI6ID");

                entity.Property(e => e.Kategori7id).HasColumnName("KATEGORI7ID");

                entity.Property(e => e.Kategori8id).HasColumnName("KATEGORI8ID");

                entity.Property(e => e.Kategori9id).HasColumnName("KATEGORI9ID");

                entity.Property(e => e.Kdvdahil).HasColumnName("KDVDAHIL");

                entity.Property(e => e.Kdvkriteri).HasColumnName("KDVKRITERI");

                entity.Property(e => e.Kod)
                    .HasColumnName("KOD")
                    .HasMaxLength(100);

                entity.Property(e => e.Kredilimit)
                    .HasColumnName("KREDILIMIT")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LedmuhMusterihesapid).HasColumnName("LEDMUH_MUSTERIHESAPID");

                entity.Property(e => e.LedmuhSaticihesapid).HasColumnName("LEDMUH_SATICIHESAPID");

                entity.Property(e => e.MobilEvraknoayritakipet).HasColumnName("MOBIL_EVRAKNOAYRITAKIPET");

                entity.Property(e => e.MobilFiyatdegistirme)
                    .HasColumnName("MOBIL_FIYATDEGISTIRME")
                    .HasMaxLength(2);

                entity.Property(e => e.MobilFiyatliirsaliye)
                    .HasColumnName("MOBIL_FIYATLIIRSALIYE")
                    .HasMaxLength(4);

                entity.Property(e => e.MobilGpskonumkisitlama)
                    .HasColumnName("MOBIL_GPSKONUMKISITLAMA")
                    .HasMaxLength(2);

                entity.Property(e => e.MobilIskontofiyatyansitma)
                    .HasColumnName("MOBIL_ISKONTOFIYATYANSITMA")
                    .HasMaxLength(2);

                entity.Property(e => e.MobilKredilimityetki)
                    .HasColumnName("MOBIL_KREDILIMITYETKI")
                    .HasMaxLength(2);

                entity.Property(e => e.MobilPartinotakip).HasColumnName("MOBIL_PARTINOTAKIP");

                entity.Property(e => e.MobilRisklimityetki)
                    .HasColumnName("MOBIL_RISKLIMITYETKI")
                    .HasMaxLength(2);

                entity.Property(e => e.MobilVadelimityetki)
                    .HasColumnName("MOBIL_VADELIMITYETKI")
                    .HasMaxLength(2);

                entity.Property(e => e.Muafiyet).HasColumnName("MUAFIYET");

                entity.Property(e => e.Musterimuhasebekodu)
                    .HasColumnName("MUSTERIMUHASEBEKODU")
                    .HasMaxLength(50);

                entity.Property(e => e.Ozelkod1)
                    .HasColumnName("OZELKOD1")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod2)
                    .HasColumnName("OZELKOD2")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod3)
                    .HasColumnName("OZELKOD3")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod4)
                    .HasColumnName("OZELKOD4")
                    .HasMaxLength(100);

                entity.Property(e => e.Ozelkod5)
                    .HasColumnName("OZELKOD5")
                    .HasMaxLength(100);

                entity.Property(e => e.Renkkodu)
                    .HasColumnName("RENKKODU")
                    .HasMaxLength(6);

                entity.Property(e => e.Risklimit)
                    .HasColumnName("RISKLIMIT")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Saticimuhasebekodu)
                    .HasColumnName("SATICIMUHASEBEKODU")
                    .HasMaxLength(50);

                entity.Property(e => e.Satisyapma).HasColumnName("SATISYAPMA");

                entity.Property(e => e.SatisyapmaSebep)
                    .HasColumnName("SATISYAPMA_SEBEP")
                    .HasMaxLength(255);

                entity.Property(e => e.Smsizin).HasColumnName("SMSIZIN");

                entity.Property(e => e.Soyad)
                    .HasColumnName("SOYAD")
                    .HasMaxLength(255);

                entity.Property(e => e.Stokiskonto1var).HasColumnName("STOKISKONTO1VAR");

                entity.Property(e => e.Stokiskonto2var).HasColumnName("STOKISKONTO2VAR");

                entity.Property(e => e.Stokiskonto3var).HasColumnName("STOKISKONTO3VAR");

                entity.Property(e => e.Subeid).HasColumnName("SUBEID");

                entity.Property(e => e.Tabelaisim)
                    .HasColumnName("TABELAISIM")
                    .HasMaxLength(255);

                entity.Property(e => e.Tapdkno)
                    .HasColumnName("TAPDKNO")
                    .HasMaxLength(50);

                entity.Property(e => e.Tel1)
                    .HasColumnName("TEL1")
                    .HasMaxLength(100);

                entity.Property(e => e.Tel2)
                    .HasColumnName("TEL2")
                    .HasMaxLength(100);

                entity.Property(e => e.Temsilciid).HasColumnName("TEMSILCIID");

                entity.Property(e => e.TerminalciktiprofilidF2).HasColumnName("TERMINALCIKTIPROFILID_F2");

                entity.Property(e => e.TerminalciktiprofilidFb).HasColumnName("TERMINALCIKTIPROFILID_FB");

                entity.Property(e => e.TerminalciktiprofilidI2).HasColumnName("TERMINALCIKTIPROFILID_I2");

                entity.Property(e => e.TerminalislemF2).HasColumnName("TERMINALISLEM_F2");

                entity.Property(e => e.TerminalislemFb).HasColumnName("TERMINALISLEM_FB");

                entity.Property(e => e.TerminalislemI2).HasColumnName("TERMINALISLEM_I2");

                entity.Property(e => e.TerminalislemKullan).HasColumnName("TERMINALISLEM_KULLAN");

                entity.Property(e => e.TerminalyetkiFiyatdegistirme).HasColumnName("TERMINALYETKI_FIYATDEGISTIRME");

                entity.Property(e => e.Ticarisicilno)
                    .HasColumnName("TICARISICILNO")
                    .HasMaxLength(50);

                entity.Property(e => e.Ulke)
                    .HasColumnName("ULKE")
                    .HasMaxLength(100);

                entity.Property(e => e.Vergidaire)
                    .HasColumnName("VERGIDAIRE")
                    .HasMaxLength(50);

                entity.Property(e => e.Vergino)
                    .HasColumnName("VERGINO")
                    .HasMaxLength(50);

                entity.Property(e => e.Web)
                    .HasColumnName("WEB")
                    .HasMaxLength(50);

                entity.Property(e => e.Yetkili)
                    .HasColumnName("YETKILI")
                    .HasMaxLength(255);

                entity.Property(e => e.Yetkiliadres)
                    .HasColumnName("YETKILIADRES")
                    .HasMaxLength(400);

                entity.Property(e => e.Zorunluparabirimi).HasColumnName("ZORUNLUPARABIRIMI");
            });

            modelBuilder.Entity<WebappLoginLog>(entity =>
            {
                entity.ToTable("webapp_login_log", "dbo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Tarih)
                    .HasColumnName("TARIH")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<WebappSepet>(entity =>
            {
                entity.ToTable("webapp_sepet", "dbo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birim)
                    .HasColumnName("BIRIM")
                    .HasMaxLength(50);

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Miktar).HasColumnName("MIKTAR");

                entity.Property(e => e.Stokid).HasColumnName("STOKID");
            });

            modelBuilder.Entity<Websepet>(entity =>
            {

                entity.ToTable("websepet", "dbo");

                entity.Property(e => e.Birim)
                    .HasColumnName("BIRIM")
                    .HasMaxLength(50);

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Miktar).HasColumnName("MIKTAR");

                entity.Property(e => e.Stokid).HasColumnName("STOKID");
            });

            modelBuilder.Entity<Websiparis>(entity =>
            {

                entity.ToTable("websiparis", "dbo");

                entity.Property(e => e.Aciklama)
                    .HasColumnName("ACIKLAMA")
                    .HasMaxLength(255);

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Siparisno)
                    .HasColumnName("SIPARISNO")
                    .HasMaxLength(20);

                entity.Property(e => e.Tarih)
                    .HasColumnName("TARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Termintarih)
                    .HasColumnName("TERMINTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Teslimtarih)
                    .HasColumnName("TESLIMTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.SevkAdresId)
                    .HasColumnName("SEVKADRESID")
                    .HasColumnType("long");
            });

            modelBuilder.Entity<Websipariskalem>(entity =>
            {

                entity.ToTable("websipariskalem", "dbo");

                entity.Property(e => e.Birimambalaj)
                    .HasColumnName("BIRIMAMBALAJ")
                    .HasMaxLength(20);

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Islemtipi).HasColumnName("ISLEMTIPI");

                entity.Property(e => e.Islemtipia)
                    .HasColumnName("ISLEMTIPIA")
                    .HasMaxLength(20);

                entity.Property(e => e.Miktarambalaj)
                    .HasColumnName("MIKTARAMBALAJ")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Siparisid).HasColumnName("SIPARISID");

                entity.Property(e => e.Stokid).HasColumnName("STOKID");

                entity.Property(e => e.Stokkod)
                    .HasColumnName("STOKKOD")
                    .HasMaxLength(100);

                entity.Property(e => e.Tarih)
                    .HasColumnName("TARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Termintarih)
                    .HasColumnName("TERMINTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Teslimtarih)
                    .HasColumnName("TESLIMTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.SevkAdresId)
                    .HasColumnName("SEVKADRESID")
                    .HasColumnType("long");
            });

            modelBuilder.Entity<Wfiyat>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("wfiyat");

                entity.Property(e => e.Aciklama)
                    .HasColumnName("ACIKLAMA")
                    .HasMaxLength(255);

                entity.Property(e => e.Aktif).HasColumnName("AKTIF");

                entity.Property(e => e.Birimcarpan)
                    .HasColumnName("BIRIMCARPAN")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Birimid).HasColumnName("BIRIMID");

                entity.Property(e => e.Carisecimtip).HasColumnName("CARISECIMTIP");

                entity.Property(e => e.Duzenlemetarih)
                    .HasColumnName("DUZENLEMETARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fiyat)
                    .HasColumnName("FIYAT")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fiyataltlisteid).HasColumnName("FIYATALTLISTEID");

                entity.Property(e => e.Fiyatlistedetayid).HasColumnName("FIYATLISTEDETAYID");

                entity.Property(e => e.Fiyatlisteid).HasColumnName("FIYATLISTEID");

                entity.Property(e => e.Ilktarih)
                    .HasColumnName("ILKTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Isim)
                    .HasColumnName("ISIM")
                    .HasMaxLength(255);

                entity.Property(e => e.Iskonto1)
                    .HasColumnName("ISKONTO1")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iskonto2)
                    .HasColumnName("ISKONTO2")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iskonto3)
                    .HasColumnName("ISKONTO3")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Islemtip)
                    .HasColumnName("ISLEMTIP")
                    .HasMaxLength(4);

                entity.Property(e => e.Kdvdahil).HasColumnName("KDVDAHIL");

                entity.Property(e => e.Kod)
                    .HasColumnName("KOD")
                    .HasMaxLength(100);

                entity.Property(e => e.Sevkadressecimtip).HasColumnName("SEVKADRESSECIMTIP");

                entity.Property(e => e.Sontarih)
                    .HasColumnName("SONTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Stokid).HasColumnName("STOKID");

                entity.Property(e => e.Tarih)
                    .HasColumnName("TARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tip)
                    .HasColumnName("TIP")
                    .HasMaxLength(4);
            });

            modelBuilder.Entity<Wsiparis>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("wsiparis");

                entity.Property(e => e.Aciklama)
                    .HasColumnName("ACIKLAMA")
                    .HasMaxLength(400);

                entity.Property(e => e.B2b).HasColumnName("B2B");

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Depoid).HasColumnName("DEPOID");

                entity.Property(e => e.Evrakno)
                    .HasColumnName("EVRAKNO")
                    .HasMaxLength(255);

                entity.Property(e => e.Evrakseri)
                    .HasColumnName("EVRAKSERI")
                    .HasMaxLength(3);

                entity.Property(e => e.Evraksira)
                    .HasColumnName("EVRAKSIRA")
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Iskonto1)
                    .HasColumnName("ISKONTO1")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iskonto2)
                    .HasColumnName("ISKONTO2")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Onay).HasColumnName("ONAY");

                entity.Property(e => e.Onaystr)
                    .HasColumnName("ONAYSTR")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Saat)
                    .HasColumnName("SAAT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Subeid).HasColumnName("SUBEID");

                entity.Property(e => e.Tarih)
                    .HasColumnName("TARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Temsilciid).HasColumnName("TEMSILCIID");

                entity.Property(e => e.Tip)
                    .HasColumnName("TIP")
                    .HasMaxLength(2);

                entity.Property(e => e.Tutar)
                    .HasColumnName("TUTAR")
                    .HasColumnType("numeric(38, 6)");

                entity.Property(e => e.Vadetarih)
                    .HasColumnName("VADETARIH")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Wsiparisdetay>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("wsiparisdetay");

                entity.Property(e => e.Aciklama)
                    .HasColumnName("ACIKLAMA")
                    .HasMaxLength(400);

                entity.Property(e => e.B2b).HasColumnName("B2B");

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Depoid).HasColumnName("DEPOID");

                entity.Property(e => e.Dovizcinsi)
                    .HasColumnName("DOVIZCINSI")
                    .HasMaxLength(3);

                entity.Property(e => e.Dovizkur)
                    .HasColumnName("DOVIZKUR")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Evrakno)
                    .HasColumnName("EVRAKNO")
                    .HasMaxLength(255);

                entity.Property(e => e.Evrakseri)
                    .HasColumnName("EVRAKSERI")
                    .HasMaxLength(3);

                entity.Property(e => e.Evraksira)
                    .HasColumnName("EVRAKSIRA")
                    .HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Iskonto1)
                    .HasColumnName("ISKONTO1")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iskonto2)
                    .HasColumnName("ISKONTO2")
                    .HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Iskonto3)
                    .HasColumnName("ISKONTO3")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Kdv)
                    .HasColumnName("KDV")
                    .HasColumnType("numeric(38, 6)");

                entity.Property(e => e.Matrah)
                    .HasColumnName("MATRAH")
                    .HasColumnType("numeric(38, 6)");

                entity.Property(e => e.Onay).HasColumnName("ONAY");

                entity.Property(e => e.Otv)
                    .HasColumnName("OTV")
                    .HasColumnType("numeric(38, 6)");

                entity.Property(e => e.Saat)
                    .HasColumnName("SAAT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Sevkadresid).HasColumnName("SEVKADRESID");

                entity.Property(e => e.Subeid).HasColumnName("SUBEID");

                entity.Property(e => e.Tarih)
                    .HasColumnName("TARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Temsilciid).HasColumnName("TEMSILCIID");

                entity.Property(e => e.Teslimtarih)
                    .HasColumnName("TESLIMTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tip)
                    .HasColumnName("TIP")
                    .HasMaxLength(2);

                entity.Property(e => e.Tutar)
                    .HasColumnName("TUTAR")
                    .HasColumnType("numeric(38, 6)");
            });

            modelBuilder.Entity<Wstokbakiye>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("wstokbakiye");

                entity.Property(e => e.Ambalajmiktar)
                    .HasColumnName("AMBALAJMIKTAR")
                    .HasColumnType("numeric(23, 6)");

                entity.Property(e => e.Depoid).HasColumnName("DEPOID");

                entity.Property(e => e.Miktar)
                    .HasColumnName("MIKTAR")
                    .HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Projeid).HasColumnName("PROJEID");

                entity.Property(e => e.Sevktarih)
                    .HasColumnName("SEVKTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Stokid).HasColumnName("STOKID");

                entity.Property(e => e.Tarih)
                    .HasColumnName("TARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Varyasyonid).HasColumnName("VARYASYONID");
            });

            modelBuilder.Entity<Wstokhareket>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("wstokhareket");

                entity.Property(e => e.Aciklama)
                    .HasColumnName("ACIKLAMA")
                    .HasMaxLength(255);

                entity.Property(e => e.Aciklama2)
                    .HasColumnName("ACIKLAMA2")
                    .HasMaxLength(400);

                entity.Property(e => e.Ambalajbirim)
                    .HasColumnName("AMBALAJBIRIM")
                    .HasMaxLength(100);

                entity.Property(e => e.Birim)
                    .HasColumnName("BIRIM")
                    .HasMaxLength(100);

                entity.Property(e => e.Birimid).HasColumnName("BIRIMID");

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.Cikanambalajmiktar)
                    .HasColumnName("CIKANAMBALAJMIKTAR")
                    .HasColumnType("numeric(21, 6)");

                entity.Property(e => e.Cikanmiktar)
                    .HasColumnName("CIKANMIKTAR")
                    .HasColumnType("numeric(37, 9)");

                entity.Property(e => e.Depohardetayid).HasColumnName("DEPOHARDETAYID");

                entity.Property(e => e.Depoharid).HasColumnName("DEPOHARID");

                entity.Property(e => e.Depoid).HasColumnName("DEPOID");

                entity.Property(e => e.Evrakno)
                    .HasColumnName("EVRAKNO")
                    .HasMaxLength(255);

                entity.Property(e => e.Evrakseri)
                    .HasColumnName("EVRAKSERI")
                    .HasMaxLength(3);

                entity.Property(e => e.Evraksira)
                    .HasColumnName("EVRAKSIRA")
                    .HasMaxLength(50);

                entity.Property(e => e.Faturadetayid).HasColumnName("FATURADETAYID");

                entity.Property(e => e.Faturaid).HasColumnName("FATURAID");

                entity.Property(e => e.Fiyat)
                    .HasColumnName("FIYAT")
                    .HasColumnType("numeric(38, 15)");

                entity.Property(e => e.Gc)
                    .HasColumnName("GC")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Girenambalajmiktar)
                    .HasColumnName("GIRENAMBALAJMIKTAR")
                    .HasColumnType("numeric(21, 6)");

                entity.Property(e => e.Girenmiktar)
                    .HasColumnName("GIRENMIKTAR")
                    .HasColumnType("numeric(37, 9)");

                entity.Property(e => e.Harozelkod)
                    .HasColumnName("HAROZELKOD")
                    .HasMaxLength(255);

                entity.Property(e => e.Kdvoran)
                    .HasColumnName("KDVORAN")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Matrah)
                    .HasColumnName("MATRAH")
                    .HasColumnType("numeric(37, 9)");

                entity.Property(e => e.Miktar)
                    .HasColumnName("MIKTAR")
                    .HasColumnType("numeric(37, 9)");

                entity.Property(e => e.Netfiyat)
                    .HasColumnName("NETFIYAT")
                    .HasColumnType("numeric(38, 17)");

                entity.Property(e => e.Projeid).HasColumnName("PROJEID");

                entity.Property(e => e.Saat)
                    .HasColumnName("SAAT")
                    .HasMaxLength(10);

                entity.Property(e => e.Sevkadresid).HasColumnName("SEVKADRESID");

                entity.Property(e => e.Sevktarih)
                    .HasColumnName("SEVKTARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Stokid).HasColumnName("STOKID");

                entity.Property(e => e.Stokvadetarih)
                    .HasColumnName("STOKVADETARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Stokvirmanid).HasColumnName("STOKVIRMANID");

                entity.Property(e => e.Subeid).HasColumnName("SUBEID");

                entity.Property(e => e.Tarih)
                    .HasColumnName("TARIH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Temsilciid).HasColumnName("TEMSILCIID");

                entity.Property(e => e.Tip)
                    .HasColumnName("TIP")
                    .HasMaxLength(4);

                entity.Property(e => e.Tutar)
                    .HasColumnName("TUTAR")
                    .HasColumnType("numeric(37, 9)");

                entity.Property(e => e.Uretimid).HasColumnName("URETIMID");
            });
        }
    }
}
