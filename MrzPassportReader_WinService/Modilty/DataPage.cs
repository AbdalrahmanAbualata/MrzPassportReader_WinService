using IdCardReader_WinService.Modilty;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IdCardReader_WinService.Modilty
{

    [DataContract(Name = "DataPage")]
    public class DataPage
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="DataPage" /> class.
        /// </summary>
        /// <param name="image">image.</param>
        /// <param name="fingerprints">fingerprints.</param>
        /// <param name="missingFingerprints">missingFingerprints.</param>
        //public Ecard(HandPositionEnum? position = default(HandPositionEnum?), Image image = default(Image), List<Fingerprint> fingerprints = default(List<Fingerprint>), List<MissingFingerprint> missingFingerprints = default(List<MissingFingerprint>))
        public DataPage(List<PassportField> fields = default(List<PassportField>))
        {
            //this.Position = position;
            //this.Image = image;
            this.PassportFields = fields;
            //this.MissingFingerprints = missingFingerprints;
        }




        /// <summary>
        /// Gets or Sets fields
        /// </summary>
        [DataMember(Name = "passportFields", EmitDefaultValue = false)]
        public List<PassportField> PassportFields { get; set; }

        /// <summary>
        /// Gets or Sets MissingFingerprints
        /// </summary>
        //[DataMember(Name = "missingFingerprints", EmitDefaultValue = false)]
        //public List<MissingFingerprint> MissingFingerprints { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Hand {\n");
            //sb.Append("  Position: ").Append(Position).Append("\n");
            //sb.Append("  Image: ").Append(Image).Append("\n");
            sb.Append("  PassportFields: ").Append(PassportFields).Append("\n");
            //sb.Append("  MissingFingerprints: ").Append(MissingFingerprints).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }




        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
