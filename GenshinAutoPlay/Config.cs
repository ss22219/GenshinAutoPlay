using System;
using System.IO;
using System.Xml.Serialization;

namespace GenshinAutoPlay
{
    [Serializable]
    public class Config
    {
        public static readonly string ConfigFile = "config.xml";

        public string HotKey { get; set; } = "F12";
        public int KeySpeed { get; set; } = 80;
        public int SpaceSpeed { get; set; } = 180;
        public string Content { get; set; } = @"
(NH)  (DHE)/  (DHE)T/R(DH)E /(DHW) (NE) /
(NH)  (DHE)/  (DHE)T/R(DH)E /(DHE) N /
(BG)  (DHE)/  (DHE)T/R(DH)E /(DHW) (BE) /
(BG)  (DHE)/  (DHE)T/R(DH)E /(DHE) B /

(XN)  (SGW)/  (SGW)R/E(SG)W /(SGQ) (XW) /
(XR)  (AF)T/E  (AF)R/W(SG)  R/(SGE) (BE) /
N HD/  (DH) / (DH) /(DH) N /
N  (DH)/  (DH) / (DH) /(DHW)Q(NJ)Q  /

(NH)  (DHE)/  (DHE)T/R(DH)E /(DHW) (NE) /
(NH)  (DHE)/  (DHE)T/R(DH)E /(DHE) N /
(BG)  (DHE)/  (DHE)T/R(DH)E /(DHW) (BE) /
(BG)  (DHE)/  (DHE)T/R(DH)E /(DHE) B /

(XN)  (SGW)/  (SGW)R/E(SG)W /(SGQ) (XW) /
(XR)  (AF)T/E  (AF)R/W(SG)  R/(SGE) (BE) /
N HD/  (DH) / (DH) /(DH) N /
N  (DH)/  (DH) / (DH) /(CNE)T(NY)T  /

第二段：

(VY)  (VNY)/  (VNY)Q/Y(VN)T /(VNT) (VY) /
(XY)  (XNY)/  (XNY)Q/Y(XN)T /(XNT) (XY) /
(NY)  (CNY)/  (CNY)Q/Y(CN)T /(CNT) (CY) /
(ZY)  (ZCN)/E (ZCN) / (ZCN)  /(ZCNE)T(ZY)T /

(VY)  (VNY)/  (VNY)Q/Y(VN)T /(VNT) (VY) /
(XY)  (XNY)/  (XNY)Q/Y(XN)T /(XNT) (XY) /
(NY)  (CNY)/  (CNY)Q/Y(CN)T /(CNT) (CE) /

(ZE)  (ZCN)/T (ZCN) / (ZCN)  /(ZCND)G(ZH)G /

第三段：
(VH)  (VNH)/  (VNH)Q/H(VN)G /(VNG) (VH) /
(XH)  (XNH)/  (XNH)Q/H(XN)G /(XNG) (XH) /
(NH)  (CNH)/  (CNH) E/W(CN)EQ W/(CNH) Q(CG) Q/
(ZJ)  (ZCN)H/D (ZCN) / (ZCN) /(ZCND)G(ZH)G /

(VH)  (VNH)/  (VN)H/R(VN)EW E/(VNW) Q(VH) Q/
(XW)  (XNW)/  (XNW)HQ/W(XN) HQ/(XNW) XE/
(NW) QH(CN)/W Q(CNH) /W(CN)QH /(CNW) Q(CH) /
(NW)QHWQ(CN)H/WQH(CNW)QH/WQ(CN)HWQH /(CNH)  /

结尾：

(NH)  (CN)/E (CN) /R C /(CNE) C /
(NW)  (CN)/E (CN) /Q C /(CNE) C /
(NH)  (CN)/E (CN) /R C /(CNE) C /
(NW)  (CN)/E (CN) /Q C /(CNE) C /
  
(CNH)  /
";

        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        if (File.Exists(ConfigFile))
                        {
                            using (var fs = File.OpenRead(ConfigFile))
                            {
                                _instance = new XmlSerializer(typeof(Config)).Deserialize(fs) as Config;
                                if (_instance.Content != null)
                                    _instance.Content = String.Join("\r\n", _instance.Content.Split('\r', '\n'));
                            }
                        }
                        else
                        {
                            _instance = new Config();
                        }
                    }
                    catch (Exception)
                    {
                        _instance = new Config();
                    }
                }
                return _instance;
            }
        }

        public static void Save()
        {
            using (var fs = new FileStream(ConfigFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                new XmlSerializer(typeof(Config)).Serialize(fs, _instance);
            }
        }

        private static Config _instance { get; set; }
    }
}
