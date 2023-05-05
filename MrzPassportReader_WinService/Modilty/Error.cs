
using IdCardReader_WinService.Modilty;
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
    [DataContract(Name = "Error")]
    public class Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassportField" /> class.
        /// </summary>
        /// <param name="dataBytes">One of dataBytes or dataUrl is required.</param>
        /// <param name="dataUrl">One of dataBytes or dataUrl is required.</param>
        public Error(int code = default(int), string message = default(string), string path = default(string))
        {
            this.Code = code;
            this.Message = message;
            this.Path = path;
        }

        /// <summary>
        /// One of dataBytes or dataUrl is required
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public int Code { get; set; }

        /// <summary>
        /// One of dataBytes or dataUrl is required
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }


        /// <summary>
        /// One of dataBytes or dataUrl is required
        /// </summary>
        /// <value>One of dataBytes or dataUrl is required</value>
        [DataMember(Name = "path", EmitDefaultValue = false)]
        public string Path { get; set; }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Field {\n");
            //sb.Append("  DataBytes: ").Append(DataBytes).Append("\n");
            //sb.Append("  Fieldname: ").Append(Fieldname).Append("\n");
            //sb.Append("  value: ").Append(Value).Append("\n");
            //sb.Append("  standardizedValue: ").Append(StandardizedValue).Append("\n");
            //sb.Append("  formattedValuee: ").Append(FormattedValuee).Append("\n");
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

