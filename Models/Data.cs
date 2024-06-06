using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace use_open_source_fast_report.Models
{
    public class Data
    {
        public List<GoiThau> GoiThau { get; set; }
        public List<MoiThau> MoiThau { get; set; }
        public List<DuThau> DuThau { get; set; }
        public List<DanhGiaChiTiet> DanhGiaChiTiets { get; set; }
    }
    public class GoiThau
    {
        public string MA { get; set; }
        public string TEN { get; set; }
        public string TEN_KHLCNT { get; set; }
        public string USERNAME { get; set; }
        public string FULLNAME { get; set; }
    }
    public class MoiThau
    {
        public string STT { get; set; }
        public string MAPL { get; set; }
        public string TENPL { get; set; }
        public string DVT { get; set; }
        public string QUYCACH {  get; set; }
        public string KHOILUONG { get; set; }
        public string DONGIA { get; set; }
        public string YEUCAU {  get; set; }

    }
    public class DuThau
    {
        public string STT { get; set; }
        public string MANHATHAU { get; set; }
        public string TENNHATHAU { get; set; }
        public string STTPL { get; set; }
        public string MAPL { get; set; }
        public string TENDUTHAU { get; set; }
        public string SOLUUHANH { get; set; }
        public string PHANLOAI { get; set; }
        public string HANGSX { get; set; }
        public string NUOCSX { get; set; }
        public string CHUSOHUU { get; set; }
        public string DVT { get; set; }
        public string QUYCACH { get; set; }
        public string SOLUONG { get; set; }
        public string DONGIA { get; set; }
        public string MAVTYT { get; set; }
        public string CHUNGLOAI { get; set; }
        public string KETLUAN { get; set; }
        public string GHICHU { get; set; }
    }
    public class DanhGiaChiTiet
    {
        public string IDNOIDUNG { get; set; }
        public string NOIDUNG { get; set; }
        public string DAT { get; set; }
        public string CHAPNHAN { get; set; }
        public string KHONGDAT { get; set; }
        public string NHANXET { get; set; }
        public string GHICHU { get; set; }
    }
}
