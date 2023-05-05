
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IdCardReader_WinService.Modilty
{

    /// <summary>
    /// IdCard Field
    /// </summary>
    [DataContract(Name = "PassportField")]
    public class PassportField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassportField" /> class.
        /// </summary>
        /// <param name="dataBytes">One of dataBytes or dataUrl is required.</param>
        /// <param name="dataUrl">One of dataBytes or dataUrl is required.</param>
        public PassportField(int status = default(int),List<int> detailedStatus=default(List<int>), byte[] dataBytes = default(byte[]), string fieldname = default(string), string value = default(string), string standardizedValue = default(string), string formattedValuee = default(string), Image image = default(Image))
        {
            //this.DataBytes = dataBytes;

           
            this.Fieldname = fieldname;
            this.Value = value;
            this.StandardizedValue = standardizedValue;
            this.FormattedValuee = formattedValuee;
            this.Image = image;
            this.Status = status;
            this.DetailedStatus = detailedStatus;
        }

        /// <summary>
        /// One of dataBytes or dataUrl is required
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        //[DataMember(Name = "dataBytes", EmitDefaultValue = false)]
        //public byte[] DataBytes { get; set; }








        /// <summary>
        /// One of dataBytes or dataUrl is required
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "fieldname", EmitDefaultValue = false)]
        public string Fieldname { get; set; }

        /// <summary>
        /// One of dataBytes or dataUrl is required
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }


        /// <summary>
        /// One of dataBytes or dataUrl is required
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "standardizedValue", EmitDefaultValue = false)]
        public string StandardizedValue { get; set; }

        /// <summary>
        /// One of dataBytes or dataUrl is required
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "formattedValuee", EmitDefaultValue = false)]
        public string FormattedValuee { get; set; }



        /// <summary>
        /// Gets or Sets Image
        /// </summary>
        [DataMember(Name = "image", EmitDefaultValue = false)]
        public Image Image { get; set; }



        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int  Status { get; set; }



        /// <summary>
        /// Gets or Sets Detailed Status
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "detailedStatus", EmitDefaultValue = false)]
        public List<int> DetailedStatus { get; set; }


        ///// <summary>
        ///// Gets or Sets Detailed Status
        ///// </summary>
        ///// <value>One of dataBytes or dataUrl is required</value>
        //[DataMember(Name = "detailedStatus", EmitDefaultValue = false)]
        //public List<int> compar { get; set; }



        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Field {\n");
            sb.Append("  Fieldname: ").Append(Fieldname).Append("\n");
            sb.Append("  value: ").Append(Value).Append("\n");
            sb.Append("  standardizedValue: ").Append(StandardizedValue).Append("\n");
            sb.Append("  formattedValuee: ").Append(FormattedValuee).Append("\n");
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
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as PassportField);
        }

        /// <summary>
        /// Returns true if FingerprintTemplate instances are equal
        /// </summary>
        /// <param name="input">Instance of FingerprintTemplate to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PassportField input)
        {
            if (input == null)
            {
                return false;
            }
            return
                //(
                //    this.DataBytes == input.DataBytes ||
                //    (this.DataBytes != null &&
                //    this.DataBytes.Equals(input.DataBytes))
                //) &&
                (
                    this.Fieldname == input.Fieldname ||
                    (this.Fieldname != null &&
                    this.Fieldname.Equals(input.Fieldname))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                //if (this.DataBytes != null)
                //{
                //    hashCode = (hashCode * 59) + this.DataBytes.GetHashCode();
                //}
                if (this.Fieldname != null)
                {
                    hashCode = (hashCode * 59) + this.Fieldname.GetHashCode();
                }
                return hashCode;
            }
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

