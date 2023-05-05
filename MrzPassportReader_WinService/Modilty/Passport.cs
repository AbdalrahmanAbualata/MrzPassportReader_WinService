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

    [DataContract(Name = "Passport")]
    public class Passport
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="Passport" /> class.
        /// </summary>
        /// <param name="side">side.</param>
        /// <param name="ecard">ecard.</param>
        /// <param name="dataPage">dataPage.</param>
        public Passport( Chip ecard = default(Chip), DataPage dataPage = default(DataPage), Image image = default(Image), Authentication authentication = default(Authentication))
        {
      
            this.Chip = ecard;
            this.DataPage = dataPage;
            this.Authentication = authentication;
            this.Image = image;
            //this.MissingFingerprints = missingFingerprints;
        }


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
        /// Gets or Sets dataPage
        /// </summary>
        [DataMember(Name = "dataPage", EmitDefaultValue = false)]
        public DataPage DataPage { get; set; }


        /// <summary>
        /// Gets or Sets ecards
        /// </summary>
        [DataMember(Name = "chip", EmitDefaultValue = false)]
        public Chip Chip { get; set; }



        /// <summary>
        /// Gets or Sets ecards
        /// </summary>
        [DataMember(Name = "authentication", EmitDefaultValue = false)]
        public Authentication Authentication { get; set; }



        /// <summary>
        /// Gets or Sets Image
        /// </summary>
        [DataMember(Name = "image", EmitDefaultValue = false)]
        public Image Image { get; set; }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Hand {\n");
            sb.Append("  Chip: ").Append(Chip).Append("\n");
            sb.Append("  DataPage: ").Append(DataPage).Append("\n");
            sb.Append("  Authentication: ").Append(Authentication).Append("\n");
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
