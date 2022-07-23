using System;
using System.Text.Json.Serialization;

namespace A2Task
{
    public class Model
    {
        [JsonPropertyName("sellerName")]
        public string SellerName { get; set; }
        [JsonPropertyName("sellerInn")]
        public string SellerInn { get; set; }
        [JsonPropertyName("buyerName")]
        public string BuyerName { get; set; }
        [JsonPropertyName("buyerInn")]
        public string BuyerInn { get; set; }
        [JsonPropertyName("woodVolumeBuyer")]
        public double WoodVolumeBuyer { get; set; }
        [JsonPropertyName("woodVolumeSeller")]
        public double WoodVolumeSeller { get; set; }
        [JsonPropertyName("dealDate")]
        public DateTime DealDate { get; set; }
        [JsonPropertyName("dealNumber")]
        public string DealNumber { get; set; }

        public Model(string sellerName, string sellerInn, string buyerName, string buyerInn, double woodVolumeBuyer, double woodVolumeSeller, DateTime dealDate, string dealNumber)
        {
            SellerName = sellerName;
            SellerInn = sellerInn;
            BuyerName = buyerName;
            BuyerInn = buyerInn;
            WoodVolumeBuyer = woodVolumeBuyer;
            WoodVolumeSeller = woodVolumeSeller;
            DealDate = dealDate;
            DealNumber = dealNumber;
        }


        public override string ToString()
        {
            return $"{nameof(SellerName)}: {SellerName}, {nameof(SellerInn)}: {SellerInn}, {nameof(BuyerName)}: {BuyerName}, {nameof(BuyerInn)}: {BuyerInn}, {nameof(WoodVolumeBuyer)}: {WoodVolumeBuyer}, {nameof(WoodVolumeSeller)}: {WoodVolumeSeller}, {nameof(DealDate)}: {DealDate}, {nameof(DealNumber)}: {DealNumber}";
        }

        protected bool Equals(Model other)
        {
            return SellerName == other.SellerName && SellerInn == other.SellerInn && BuyerName == other.BuyerName && BuyerInn == other.BuyerInn && WoodVolumeBuyer.Equals(other.WoodVolumeBuyer) && WoodVolumeSeller.Equals(other.WoodVolumeSeller) && DealDate.Day == other.DealDate.Day && DealDate.Month == other.DealDate.Month && DealDate.Year == other.DealDate.Year && DealNumber == other.DealNumber;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Model) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (SellerName != null ? SellerName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SellerInn != null ? SellerInn.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BuyerName != null ? BuyerName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BuyerInn != null ? BuyerInn.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ WoodVolumeBuyer.GetHashCode();
                hashCode = (hashCode * 397) ^ WoodVolumeSeller.GetHashCode();
                hashCode = (hashCode * 397) ^ DealDate.Day.GetHashCode();
                hashCode = (hashCode * 397) ^ DealDate.Month.GetHashCode();
                hashCode = (hashCode * 397) ^ DealDate.Year.GetHashCode();
                hashCode = (hashCode * 397) ^ (DealNumber != null ? DealNumber.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}