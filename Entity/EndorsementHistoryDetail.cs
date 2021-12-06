using System.Collections.Generic;

namespace Entity
{
    public class EndorsementHistoryDetail
    {
        public string EndorsItem { get; set; }
        public string DataSalahDesc { get; set; }
        public string DataBenarDesc { get; set; }
    }
    public static class DummyDataEndorsementHistoryDetail
    {
        public static List<EndorsementHistoryDetail> ListDetailEndorsementHistoryDetail()
        {
            var result = new List<EndorsementHistoryDetail>
            {
                new EndorsementHistoryDetail
                {
                    EndorsItem = "Serial No 1",
                    DataBenarDesc = "benar",
                    DataSalahDesc = "salah"

                },
                new EndorsementHistoryDetail
                {
                    EndorsItem = "Serial No 1",
                    DataBenarDesc = "benar",
                    DataSalahDesc = "salah"

                },
                new EndorsementHistoryDetail
                {
                    EndorsItem = "Serial No 1",
                    DataBenarDesc = "qq",
                    DataSalahDesc = "salah"

                },
                new EndorsementHistoryDetail
                {
                    EndorsItem = "Serial No 2",
                    DataBenarDesc = "qq",
                    DataSalahDesc = "salah"

                },
                new EndorsementHistoryDetail
                {
                    EndorsItem = "Serial No 1",
                    DataBenarDesc = "benar",
                    DataSalahDesc = "salah"

                },
                new EndorsementHistoryDetail
                {
                    EndorsItem = "Serial No 1",
                    DataBenarDesc = "benar",
                    DataSalahDesc = "salah"

                }
            };
            return result;
        }
    }
}
