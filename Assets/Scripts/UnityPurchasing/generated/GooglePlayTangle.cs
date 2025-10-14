// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("aqccwYsDC1E97FjlzTzzv0cWBYhv05szZitDsF82dREegt1SzwKsBl/hGq1D7BI29ZmoKH8qAs0Ov3zhwCHbvXhoI6Ck+0XEiOa77/s1IPQ1EthLSbh7/jWgaXYatDM7IBCqEdtV8q7UABy1Uf0z75CeygJk1F1dcfjp2IBJOSztavNZk662v4xFbbo0t7m2hjS3vLQ0t7e2ZYl3L4aTErzA+Hr+6T1+ysPxQuWk0yaf1hCP/+pApacnECs0SOomuvUyS4+Js5SGNLeUhruwv5ww/jBBu7e3t7O2tSHfmCVnJuDran3N91H+ELTY2ZpkBIfict56DUpItsYvD0MoEv3Q9LTNHZdSRoS6CJaDUAVVjZdZecetSrMZojlvevkPV7S1t7a3");
        private static int[] order = new int[] { 12,2,4,11,5,6,11,11,9,13,12,11,12,13,14 };
        private static int key = 182;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
