using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HOTCiOSLibrary.Models
{
	public enum USStates
	{
		[Description("Alabama")]
		AL,

		[Description("Alaska")]
		AK,

		[Description("Arkansas")]
		AR,

		[Description("Arizona")]
		AZ,

		[Description("California")]
		CA,

		[Description("Colorado")]
		CO,

		[Description("Connecticut")]
		CT,

		[Description("D.C.")]
		DC,

		[Description("Delaware")]
		DE,

		[Description("Florida")]
		FL,

		[Description("Georgia")]
		GA,

		[Description("Hawaii")]
		HI,

		[Description("Iowa")]
		IA,

		[Description("Idaho")]
		ID,

		[Description("Illinois")]
		IL,

		[Description("Indiana")]
		IN,

		[Description("Kansas")]
		KS,

		[Description("Kentucky")]
		KY,

		[Description("Louisiana")]
		LA,

		[Description("Massachusetts")]
		MA,

		[Description("Maryland")]
		MD,

		[Description("Maine")]
		ME,

		[Description("Michigan")]
		MI,

		[Description("Minnesota")]
		MN,

		[Description("Missouri")]
		MO,

		[Description("Mississippi")]
		MS,

		[Description("Montana")]
		MT,

		[Description("North Carolina")]
		NC,

		[Description("North Dakota")]
		ND,

		[Description("Nebraska")]
		NE,

		[Description("New Hampshire")]
		NH,

		[Description("New Jersey")]
		NJ,

		[Description("New Mexico")]
		NM,

		[Description("Nevada")]
		NV,

		[Description("New York")]
		NY,

		[Description("Oklahoma")]
		OK,

		[Description("Ohio")]
		OH,

		[Description("Oregon")]
		OR,

		[Description("Pennsylvania")]
		PA,

		[Description("Rhode Island")]
		RI,

		[Description("South Carolina")]
		SC,

		[Description("South Dakota")]
		SD,

		[Description("Tennessee")]
		TN,

		[Description("Texas")]
		TX,

		[Description("Utah")]
		UT,

		[Description("Virginia")]
		VA,

		[Description("Vermont")]
		VT,

		[Description("Washington")]
		WA,

		[Description("Wisconsin")]
		WI,

		[Description("West Virginia")]
		WV,

		[Description("Wyoming")]
		WY
	}

	public enum Country
	{
	//	Afghanistan,
	//Albania,
	//Algeria,
	//American Samoa,
	//Andorra,
	//Angola,
	//Anguilla,
	//Antarctica,
	//Antigua and Barbuda,
	//Argentina,
	//Armenia,
	//Aruba,
	//Australia,
	//Austria,
	//Azerbaijan,
	//Bahamas,
	//Bahrain,
	//Bangladesh,
	//Barbados,
	//Belarus,
	//Belgium,
	//Belize,
	//Benin,
	//Bermuda,
	//Bhutan,
	//Bolivia,
	//Bosnia and Herzegovina,
	//Botswana,
	//Bouvet Island,
	//Brazil,
	//British Indian Ocean Territory,
	//Brunei Darussalam,
	//Bulgaria,
	//Burkina Faso,
	//Burundi,
	//Cambodia,
	//Cameroon,
	//Canada,
	//Cape Verde,
	//Cayman Islands,
	//Central African Republic,
	//Chad,
	//Chile,
	//China,
	//Christmas Island,
	//Cocos (Keeling) Islands,
	//Colombia,
	//Comoros,
	//Congo,
	//Congo, the Democratic Republic of the,
	//Cook Islands,
	//Costa Rica,
	//Cote D'Ivoire,
	//Croatia,
	//Cuba,
	//Cyprus,
	//Czech Republic,
	//Denmark,
	//Djibouti,
	//Dominica,
	//Dominican Republic,
	//Ecuador,
	//Egypt,
	//El Salvador,
	//Equatorial Guinea,
	//Eritrea,
	//Estonia,
	//Ethiopia,
	//Falkland Islands (Malvinas),
	//Faroe Islands,
	//Fiji,
	//Finland,
	//France,
	//French Guiana,
	//French Polynesia,
	//French Southern Territories,
	//Gabon,
	//Gambia,
	//Georgia,
	//Germany,
	//Ghana,
	//Gibraltar,
	//Greece,
	//Greenland,
	//Grenada,
	//Guadeloupe,
	//Guam,
	//Guatemala,
	//Guinea,
	//Guinea-Bissau,
	//Guyana,
	//Haiti,
	//Heard Island and Mcdonald Islands,
	//Holy See (Vatican City State),
	//Honduras,
	//Hong Kong,
	//Hungary,
	//Iceland,
	//India,
	//Indonesia,
	//Iran, Islamic Republic of,
	//Iraq,
	//Ireland,
	//Israel,
	//Italy,
	//Jamaica,
	//Japan,
	//Jordan,
	//Kazakhstan,
	//Kenya,
	//Kiribati,
	//Korea, Democratic People's Republic of,
	//Korea, Republic of,
	//Kuwait,
	//Kyrgyzstan,
	//Lao People's Democratic Republic,
	//Latvia,
	//Lebanon,
	//Lesotho,
	//Liberia,
	//Libyan Arab Jamahiriya,
	//Liechtenstein,
	//Lithuania,
	//Luxembourg,
	//Macao,
	//Macedonia, the Former Yugoslav Republic of,
	//Madagascar,
	//Malawi,
	//Malaysia,
	//Maldives,
	//Mali,
	//Malta,
	//Marshall Islands,
	//Martinique,
	//Mauritania,
	//Mauritius,
	//Mayotte,
	//Mexico,
	//Micronesia, Federated States of,
	//Moldova, Republic of,
	//Monaco,
	//Mongolia,
	//Montserrat,
	//Morocco,
	//Mozambique,
	//Myanmar,
	//Namibia,
	//Nauru,
	//Nepal,
	//Netherlands,
	//Netherlands Antilles,
	//New Caledonia,
	//New Zealand,
	//Nicaragua,
	//Niger,
	//Nigeria,
	//Niue,
	//Norfolk Island,
	//Northern Mariana Islands,
	//Norway,
	//Oman,
	//Pakistan,
	//Palau,
	//Palestinian Territory, Occupied,
	//Panama,
	//Papua New Guinea,
	//Paraguay,
	//Peru,
	//Philippines,
	//Pitcairn,
	//Poland,
	//Portugal,
	//Puerto Rico,
	//Qatar,
	//Reunion,
	//Romania,
	//Russian Federation,
	//Rwanda,
	//Saint Helena,
	//Saint Kitts and Nevis,
	//Saint Lucia,
	//Saint Pierre and Miquelon,
	//Saint Vincent and the Grenadines,
	//Samoa,
	//San Marino,
	//Sao Tome and Principe,
	//Saudi Arabia,
	//Senegal,
	//Serbia and Montenegro,
	//Seychelles,
	//Sierra Leone,
	//Singapore,
	//Slovakia,
	//Slovenia,
	//Solomon Islands,
	//Somalia,
	//South Africa,
	//South Georgia and the South Sandwich Islands,
	//Spain,
	//Sri Lanka,
	//Sudan,
	//Suriname,
	//Svalbard and Jan Mayen,
	//Swaziland,
	//Sweden,
	//Switzerland,
	//Syrian Arab Republic,
	//Taiwan, Province of China,
	//Tajikistan,
	//Tanzania, United Republic of,
	//Thailand,
	//Timor-Leste,
	//Togo,
	//Tokelau,
	//Tonga,
	//Trinidad and Tobago,
	//Tunisia,
	//Turkey,
	//Turkmenistan,
	//Turks and Caicos Islands,
	//Tuvalu,
	//Uganda,
	//Ukraine,
	//United Arab Emirates,
	//United Kingdom,
	//United States,
	//United States Minor Outlying Islands,
	//Uruguay,
	//Uzbekistan,
	//Vanuatu,
	//Venezuela,
	//Viet Nam,
	//Virgin Islands, British,
	//Virgin Islands, US,
	//Wallis and Futuna,
	//Western Sahara,
	//Yemen,
	//Zambia,
	//Zimbabwe,
	}

	public class Event
	{
		public int ID { get; set; }
		public string EventName { get; set; }
		public string City { get; set; }
		public USStates State { get; set; }
		public DateTime DateOfEvent { get; set; }
		public byte[] Picture { get; set; }
		public double Lat { get; set; }
		public double Long { get; set; }
		public string Address { get; set; }
		public int ZipCode { get; set; }
		public bool isPublic { get; set; }
		public string Description { get; set; }
		public Country Country { get; set; }
		//public ApplicationUser { get; set; }

}
}