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

    [DataContract(Name = "Chip")]
    public class Chip
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="Hand" /> class.
        /// </summary>
        /// <param name="image">image.</param>
        /// <param name="fingerprints">fingerprints.</param>
        /// <param name="missingFingerprints">missingFingerprints.</param>
        //public Ecard(HandPositionEnum? position = default(HandPositionEnum?), Image image = default(Image), List<Fingerprint> fingerprints = default(List<Fingerprint>), List<MissingFingerprint> missingFingerprints = default(List<MissingFingerprint>))
        public Chip(List<PassportField> fields = default(List<PassportField>))
        {

            this.PassportFields = fields;
        }

        /// <summary>
        /// Gets or Sets Image
        /// </summary>
        //[DataMember(Name = "image", EmitDefaultValue = false)]
        //public Image Image { get; set; }


        /// <summary>
        /// Gets or Sets fields
        /// </summary>
        [DataMember(Name = "passportFields", EmitDefaultValue = false)]
        public List<PassportField> PassportFields { get; set; }

        /// <summary>
        /// Gets or Sets FirstName
        /// </summary>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets SecondName
        /// </summary>
        [DataMember(Name = "secondName", EmitDefaultValue = false)]
        public string SecondName { get; set; }

        /// <summary>
        /// Gets or Sets SecondName
        /// </summary>
        [DataMember(Name = "thirdName", EmitDefaultValue = false)]
        public string ThirdName { get; set; }


        /// <summary>
        /// Gets or Sets surname
        /// </summary>
        [DataMember(Name = "surname", EmitDefaultValue = false)]
        public string Surname { get; set; }


        /// <summary>
        /// Gets or Sets MotherNmae
        /// </summary>
        [DataMember(Name = "motherNmae", EmitDefaultValue = false)]
        public string MotherNmae { get; set; }

        /// <summary>
        /// Gets or Sets MotherFather
        /// </summary>
        [DataMember(Name = "motherFatherName", EmitDefaultValue = false)]
        public string MotherFatherName { get; set; }




        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Hand {\n");
            sb.Append("  PassportFields: ").Append(PassportFields).Append("\n");
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
