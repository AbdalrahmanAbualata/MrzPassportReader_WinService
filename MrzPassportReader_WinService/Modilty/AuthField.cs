
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
    [DataContract(Name = "AuthField")]
    public class AuthField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassportField" /> class.
        /// </summary>
        /// <param name="dataBytes">One of dataBytes or dataUrl is required.</param>
        /// <param name="dataUrl">One of dataBytes or dataUrl is required.</param>
        public AuthField(string fieldName = default(string), int FieldId = default(int),int authStatus = default(int))
        {
           this.FieldName = fieldName;
           this.FieldId = FieldId;
            this.AuthStatus = authStatus;

          
        }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "fieldName", EmitDefaultValue = false)]
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or Sets Detailed Status
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "fieldId", EmitDefaultValue = false)]
        public int FieldId { get; set; }

        /// <summary>
        /// Gets or Sets Detailed Status
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "authStatus", EmitDefaultValue = false)]
        public int AuthStatus { get; set; }

        /// <summary>
        /// Gets or Sets Detailed Status
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "authError", EmitDefaultValue = false)]
        public string AuthError{ get; set; }


        /// <summary>
        /// Gets or Sets Detailed Status
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "authErrorCode", EmitDefaultValue = false)]
        public int AuthErrorCode { get; set; }





        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Field {\n");
            sb.Append("  FieldName: ").Append(FieldName).Append("\n");
            sb.Append("  FieldId: ").Append(FieldId).Append("\n");
            sb.Append("  AuthStatus: ").Append(AuthStatus).Append("\n");
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
        //public override bool Equals(object input)
        //{
        //    return this.Equals(input as PassportField);
        //}

        ///// <summary>
        ///// Returns true if FingerprintTemplate instances are equal
        ///// </summary>
        ///// <param name="input">Instance of FingerprintTemplate to be compared</param>
        ///// <returns>Boolean</returns>
        //public bool Equals(PassportField input)
        //{
        //    if (input == null)
        //    {
        //        return false;
        //    }
        //    return
        //        //(
        //        //    this.DataBytes == input.DataBytes ||
        //        //    (this.DataBytes != null &&
        //        //    this.DataBytes.Equals(input.DataBytes))
        //        //) &&
        //        (
        //            this.Fieldname == input.Fieldname ||
        //            (this.Fieldname != null &&
        //            this.Fieldname.Equals(input.Fieldname))
        //        );
        //}

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        //public override int GetHashCode()
        //{
        //    unchecked // Overflow is fine, just wrap
        //    {
        //        int hashCode = 41;
        //        //if (this.DataBytes != null)
        //        //{
        //        //    hashCode = (hashCode * 59) + this.DataBytes.GetHashCode();
        //        //}
        //        if (this.Fieldname != null)
        //        {
        //            hashCode = (hashCode * 59) + this.Fieldname.GetHashCode();
        //        }
        //        return hashCode;
        //    }
        //}

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

