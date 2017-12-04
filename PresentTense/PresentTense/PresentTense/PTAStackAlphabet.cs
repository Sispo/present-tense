using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentTense
{
    public class PTAStackAlphabet
    {
        public string S = "<S>";
        public string SB = "<SB>";
        public string B = "<B>";

        public string SBP = "<SBP>";
        public string SBS = "<SBS>";

        public string SBWPS = "<SBWPS>";
        public string SBWPP = "<SBWPP>";

        public string BS = "<BS>";
        public string BP = "<BP>";

        public string VS = "<VS>";
        public string VP = "<VP>";

        public string AND = "<AND>";
        public string THE = "<THE>";
        public string ENOUGH = "<ENOUGH>";
        public string VERY = "<VERY>";
        public string TOO = "<TOO>";
        public string VERYTOO = "<VERYTOO>";
        public string EVERY = "<EVERY>";

        public string ADJCTV = "<ADJCTV>";

        public string ARFPNS = "<ARFPNS>";
        public string ARFPNP = "<ARFPNP>";
        public string BFNS = "<BFNS>";
        public string BFNP = "<BFNP>";
        public string BWPNS = "<BWPNS>";
        public string BWPNP = "<BWPNP>";
        public string LPNS = "<LPNS>";
        public string LPNP = "<LPNP>";
        public string OPNS = "<OPNS>";
        public string OPNP = "<OPNP>";
        public string PNS = "<PNS>";
        public string PNP = "<PNP>";
        public string UGPNS = "<UGPNS>";
        public string UGPNP = "<UGPNP>";
        public string PLACENS = "<PLACENS>";
        public string PLACENP = "<PLACENP>";
        public string PLACEN = "<PLACEN>";

        public string NM = "<NM>";
        public string NMB = "<NMB>";
        public string NMG = "<NMG>";

        public string MNS = "<MNS>";
        public string MNP = "<MNP>";
        public string NS = "<NS>";
        public string NP = "<NP>";
        
        public string TNS = "<TNS>";
        public string TNP = "<TNP>";

        public string IYWT = "<IYWT>";
        public string HSI = "<HSI>";

        public string I = "<I>";
        public string YWT = "<YWT>";
        public string YOU = "<YOU>";
        public string WE = "<WE>";
        public string THEY = "<THEY>";

        public string HE = "<HE>";
        public string SHE = "<SHE>";
        public string IT = "<IT>";

        public string C = "<C>";
        public string MPC = "<MPC>";
        public string USAC = "<USAC>";
        public string UKC = "<UKC>";

        public string SBADVMVS = "<SBADVMVS>";
        public string SBADVMVP = "<SBADVMVP>";

        public string SBADVFVS = "<SBADVFVS>";
        public string SBADVFVP = "<SBADVFVP>";

        public string ADVOFM = "<ADVOFM>";
        public string ADVOFF = "<ADVOFF>";
        public string ADVOFD = "<ADVOFD>";
        public string ADVOFP = "<ADVOFP>";
        public string ADVOFPP = "<ADVOFPP>";

        public string DMPF = "<DMPF>";
        public string ADVPB = "<ADVPB>";
        public string ADVF = "<ADVF>";
        public string ADVD = "<ADVD>";
        public string ADVM = "<ADVM>";
        public string ADVP = "<ADVP>";
        


        public List<string> allValues
        {
            get
            {
                var values = new List<string>();

                values.Add(S);
                values.Add(SB);
                values.Add(B);
                values.Add(SBP);
                values.Add(SBS);
                values.Add(BS);
                values.Add(BP);
                values.Add(IYWT);
                values.Add(HSI);
                values.Add(VS);
                values.Add(VP);
                values.Add(AND);
                values.Add(THE);
                values.Add(ADJCTV);
                values.Add(ARFPNS);
                values.Add(ARFPNP);
                values.Add(BFNS);
                values.Add(BFNP);
                values.Add(BWPNS);
                values.Add(BWPNP);
                values.Add(NM);
                values.Add(NMB);
                values.Add(NMG);
                values.Add(LPNS);
                values.Add(LPNP);
                values.Add(NS);
                values.Add(NP);
                values.Add(OPNS);
                values.Add(OPNP);
                values.Add(PNS);
                values.Add(PNP);
                values.Add(PLACENS);
                values.Add(PLACENP);
                values.Add(TNS);
                values.Add(TNP);
                values.Add(UGPNS);
                values.Add(UGPNP);
                values.Add(USAC);
                values.Add(SBWPP);
                values.Add(SBWPS);
                values.Add(I);
                values.Add(YOU);
                values.Add(WE);
                values.Add(THEY);
                values.Add(HE);
                values.Add(SHE);
                values.Add(IT);
                values.Add(SBADVMVS);
                values.Add(SBADVMVP);
                values.Add(SBADVFVS);
                values.Add(SBADVFVP);
                values.Add(ADVOFM);
                values.Add(ADVOFF);
                values.Add(MNS);
                values.Add(MNP);
                values.Add(MPC);
                values.Add(UKC);
                values.Add(C);
                values.Add(ADVOFD);
                values.Add(ADVOFP);
                values.Add(ADVOFPP);
                values.Add(YWT);

                return values;
            }
        }

    }
}
