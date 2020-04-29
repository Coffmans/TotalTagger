using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TotalTagger
{
    public class MusicBrainzData
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://musicbrainz.org/ns/mmd-2.0#", IsNullable = false)]
        public partial class metadata
        {

            private metadataReleaselist releaselistField;

            private System.DateTime createdField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("release-list")]
            public metadataReleaselist releaselist
            {
                get
                {
                    return this.releaselistField;
                }
                set
                {
                    this.releaselistField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime created
            {
                get
                {
                    return this.createdField;
                }
                set
                {
                    this.createdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselist
        {

            private metadataReleaselistRelease[] releaseField;

            private byte countField;

            private byte offsetField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("release")]
            public metadataReleaselistRelease[] release
            {
                get
                {
                    return this.releaseField;
                }
                set
                {
                    this.releaseField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte count
            {
                get
                {
                    return this.countField;
                }
                set
                {
                    this.countField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte offset
            {
                get
                {
                    return this.offsetField;
                }
                set
                {
                    this.offsetField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistRelease
        {

            private string titleField;

            private string statusField;

            private string packagingField;

            private metadataReleaselistReleaseTextrepresentation textrepresentationField;

            private metadataReleaselistReleaseArtistcredit artistcreditField;

            private metadataReleaselistReleaseReleasegroup releasegroupField;

            private System.DateTime dateField;

            private string countryField;

            private metadataReleaselistReleaseReleaseeventlist releaseeventlistField;

            private ulong barcodeField;

            private bool barcodeFieldSpecified;

            private metadataReleaselistReleaseLabelinfolist labelinfolistField;

            private metadataReleaselistReleaseMediumlist mediumlistField;

            private object taglistField;

            private string idField;

            private byte scoreField;

            /// <remarks/>
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            public string status
            {
                get
                {
                    return this.statusField;
                }
                set
                {
                    this.statusField = value;
                }
            }

            /// <remarks/>
            public string packaging
            {
                get
                {
                    return this.packagingField;
                }
                set
                {
                    this.packagingField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("text-representation")]
            public metadataReleaselistReleaseTextrepresentation textrepresentation
            {
                get
                {
                    return this.textrepresentationField;
                }
                set
                {
                    this.textrepresentationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("artist-credit")]
            public metadataReleaselistReleaseArtistcredit artistcredit
            {
                get
                {
                    return this.artistcreditField;
                }
                set
                {
                    this.artistcreditField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("release-group")]
            public metadataReleaselistReleaseReleasegroup releasegroup
            {
                get
                {
                    return this.releasegroupField;
                }
                set
                {
                    this.releasegroupField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
            public System.DateTime date
            {
                get
                {
                    return this.dateField;
                }
                set
                {
                    this.dateField = value;
                }
            }

            /// <remarks/>
            public string country
            {
                get
                {
                    return this.countryField;
                }
                set
                {
                    this.countryField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("release-event-list")]
            public metadataReleaselistReleaseReleaseeventlist releaseeventlist
            {
                get
                {
                    return this.releaseeventlistField;
                }
                set
                {
                    this.releaseeventlistField = value;
                }
            }

            /// <remarks/>
            public ulong barcode
            {
                get
                {
                    return this.barcodeField;
                }
                set
                {
                    this.barcodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool barcodeSpecified
            {
                get
                {
                    return this.barcodeFieldSpecified;
                }
                set
                {
                    this.barcodeFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("label-info-list")]
            public metadataReleaselistReleaseLabelinfolist labelinfolist
            {
                get
                {
                    return this.labelinfolistField;
                }
                set
                {
                    this.labelinfolistField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("medium-list")]
            public metadataReleaselistReleaseMediumlist mediumlist
            {
                get
                {
                    return this.mediumlistField;
                }
                set
                {
                    this.mediumlistField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("tag-list")]
            public object taglist
            {
                get
                {
                    return this.taglistField;
                }
                set
                {
                    this.taglistField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://musicbrainz.org/ns/ext#-2.0")]
            public byte score
            {
                get
                {
                    return this.scoreField;
                }
                set
                {
                    this.scoreField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseTextrepresentation
        {

            private string languageField;

            private string scriptField;

            /// <remarks/>
            public string language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
                }
            }

            /// <remarks/>
            public string script
            {
                get
                {
                    return this.scriptField;
                }
                set
                {
                    this.scriptField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseArtistcredit
        {

            private metadataReleaselistReleaseArtistcreditNamecredit namecreditField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("name-credit")]
            public metadataReleaselistReleaseArtistcreditNamecredit namecredit
            {
                get
                {
                    return this.namecreditField;
                }
                set
                {
                    this.namecreditField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseArtistcreditNamecredit
        {

            private metadataReleaselistReleaseArtistcreditNamecreditArtist artistField;

            /// <remarks/>
            public metadataReleaselistReleaseArtistcreditNamecreditArtist artist
            {
                get
                {
                    return this.artistField;
                }
                set
                {
                    this.artistField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseArtistcreditNamecreditArtist
        {

            private string nameField;

            private string sortnameField;

            private string idField;

            /// <remarks/>
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("sort-name")]
            public string sortname
            {
                get
                {
                    return this.sortnameField;
                }
                set
                {
                    this.sortnameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseReleasegroup
        {

            private string titleField;

            private metadataReleaselistReleaseReleasegroupPrimarytype primarytypeField;

            private metadataReleaselistReleaseReleasegroupSecondarytypelist secondarytypelistField;

            private string idField;

            private string typeField;

            private string typeidField;

            /// <remarks/>
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("primary-type")]
            public metadataReleaselistReleaseReleasegroupPrimarytype primarytype
            {
                get
                {
                    return this.primarytypeField;
                }
                set
                {
                    this.primarytypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("secondary-type-list")]
            public metadataReleaselistReleaseReleasegroupSecondarytypelist secondarytypelist
            {
                get
                {
                    return this.secondarytypelistField;
                }
                set
                {
                    this.secondarytypelistField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute("type-id")]
            public string typeid
            {
                get
                {
                    return this.typeidField;
                }
                set
                {
                    this.typeidField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseReleasegroupPrimarytype
        {

            private string idField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseReleasegroupSecondarytypelist
        {

            private metadataReleaselistReleaseReleasegroupSecondarytypelistSecondarytype secondarytypeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("secondary-type")]
            public metadataReleaselistReleaseReleasegroupSecondarytypelistSecondarytype secondarytype
            {
                get
                {
                    return this.secondarytypeField;
                }
                set
                {
                    this.secondarytypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseReleasegroupSecondarytypelistSecondarytype
        {

            private string idField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseReleaseeventlist
        {

            private metadataReleaselistReleaseReleaseeventlistReleaseevent releaseeventField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("release-event")]
            public metadataReleaselistReleaseReleaseeventlistReleaseevent releaseevent
            {
                get
                {
                    return this.releaseeventField;
                }
                set
                {
                    this.releaseeventField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseReleaseeventlistReleaseevent
        {

            private System.DateTime dateField;

            private metadataReleaselistReleaseReleaseeventlistReleaseeventArea areaField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
            public System.DateTime date
            {
                get
                {
                    return this.dateField;
                }
                set
                {
                    this.dateField = value;
                }
            }

            /// <remarks/>
            public metadataReleaselistReleaseReleaseeventlistReleaseeventArea area
            {
                get
                {
                    return this.areaField;
                }
                set
                {
                    this.areaField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseReleaseeventlistReleaseeventArea
        {

            private string nameField;

            private string sortnameField;

            private metadataReleaselistReleaseReleaseeventlistReleaseeventAreaIso31661codelist iso31661codelistField;

            private string idField;

            /// <remarks/>
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("sort-name")]
            public string sortname
            {
                get
                {
                    return this.sortnameField;
                }
                set
                {
                    this.sortnameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("iso-3166-1-code-list")]
            public metadataReleaselistReleaseReleaseeventlistReleaseeventAreaIso31661codelist iso31661codelist
            {
                get
                {
                    return this.iso31661codelistField;
                }
                set
                {
                    this.iso31661codelistField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseReleaseeventlistReleaseeventAreaIso31661codelist
        {

            private string iso31661codeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("iso-3166-1-code")]
            public string iso31661code
            {
                get
                {
                    return this.iso31661codeField;
                }
                set
                {
                    this.iso31661codeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseLabelinfolist
        {

            private metadataReleaselistReleaseLabelinfolistLabelinfo labelinfoField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("label-info")]
            public metadataReleaselistReleaseLabelinfolistLabelinfo labelinfo
            {
                get
                {
                    return this.labelinfoField;
                }
                set
                {
                    this.labelinfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseLabelinfolistLabelinfo
        {

            private metadataReleaselistReleaseLabelinfolistLabelinfoLabel labelField;

            /// <remarks/>
            public metadataReleaselistReleaseLabelinfolistLabelinfoLabel label
            {
                get
                {
                    return this.labelField;
                }
                set
                {
                    this.labelField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseLabelinfolistLabelinfoLabel
        {

            private string nameField;

            private string idField;

            /// <remarks/>
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseMediumlist
        {

            private byte trackcountField;

            private metadataReleaselistReleaseMediumlistMedium mediumField;

            private byte countField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("track-count")]
            public byte trackcount
            {
                get
                {
                    return this.trackcountField;
                }
                set
                {
                    this.trackcountField = value;
                }
            }

            /// <remarks/>
            public metadataReleaselistReleaseMediumlistMedium medium
            {
                get
                {
                    return this.mediumField;
                }
                set
                {
                    this.mediumField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte count
            {
                get
                {
                    return this.countField;
                }
                set
                {
                    this.countField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseMediumlistMedium
        {

            private string formatField;

            private metadataReleaselistReleaseMediumlistMediumDisclist disclistField;

            private metadataReleaselistReleaseMediumlistMediumTracklist tracklistField;

            /// <remarks/>
            public string format
            {
                get
                {
                    return this.formatField;
                }
                set
                {
                    this.formatField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("disc-list")]
            public metadataReleaselistReleaseMediumlistMediumDisclist disclist
            {
                get
                {
                    return this.disclistField;
                }
                set
                {
                    this.disclistField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("track-list")]
            public metadataReleaselistReleaseMediumlistMediumTracklist tracklist
            {
                get
                {
                    return this.tracklistField;
                }
                set
                {
                    this.tracklistField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseMediumlistMediumDisclist
        {

            private byte countField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte count
            {
                get
                {
                    return this.countField;
                }
                set
                {
                    this.countField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
        public partial class metadataReleaselistReleaseMediumlistMediumTracklist
        {

            private byte countField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte count
            {
                get
                {
                    return this.countField;
                }
                set
                {
                    this.countField = value;
                }
            }
        }


    }
    class MusicBrainz : TotalTagger.MusicAPIs.TagService
    {
        public bool QueryForMetadataNonAsync(System.Threading.CancellationToken cancellationToken, int limit = 25)
        {
            if (String.IsNullOrEmpty(ExistingMetadata.Title))
            {
                QueryResult = "Title Needed To Perform Search From MusicBrainz";
                ResultOfQuery = false;
                return false;
            }

            string encodedTitle = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Title);
            if (String.IsNullOrEmpty(encodedTitle))
            {
                QueryResult = "Title Needed To Perform Search From MusicBrainz";
                ResultOfQuery = false;
                return false;
            }

            string encodedArtist = "";
            string query = "";
            if (!String.IsNullOrEmpty(ExistingMetadata.Artist))
            {
                string searchableArtist = ExistingMetadata.Artist;
                int nIndex = searchableArtist.IndexOf("feat");
                if (nIndex > 0)
                    searchableArtist = searchableArtist.Remove(nIndex);

                if (searchableArtist.IndexOfAny(new char[] { '/', '(', '&' }) > 0)
                {
                    nIndex = searchableArtist.IndexOf("(");
                    if (nIndex > 0)
                        searchableArtist = searchableArtist.Remove(nIndex);

                    nIndex = searchableArtist.IndexOf("/");
                    if (nIndex > 0)
                        searchableArtist = searchableArtist.Remove(nIndex);

                    nIndex = searchableArtist.IndexOf("&");
                    if (nIndex > 0)
                        searchableArtist = searchableArtist.Remove(nIndex);
                }
                searchableArtist = searchableArtist.TrimEnd();

                encodedArtist = System.Web.HttpUtility.UrlEncode(searchableArtist);
                query = Properties.Settings.Default.MBRecordingUrl + "recording:" + encodedTitle + "%20AND%20artist:" + encodedArtist;
            }
            else
            {
                query = Properties.Settings.Default.MBRecordingUrl + "recording:" + encodedTitle;
            }

            try
            {
                string fullUserAgent = $"{Properties.Settings.Default.MBBaseUrl} {Properties.Settings.Default.MBBaseUrl}/{Properties.Settings.Default.MBVersion} ({Properties.Settings.Default.MBUserAgentUrl})";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                request.UserAgent = fullUserAgent;
                request.Accept = "application/xml";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (results != null)
                {
                    XElement root = XElement.Parse(results);
                    XNamespace nameSpace = root.Name.Namespace;
                    var recordinglist = root.Element(nameSpace + "recording-list");
                    if (recordinglist == null)
                    {
                        QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from MusicBrainz";
                        ResultOfQuery = false;
                        return false;
                    }

                    var recording = recordinglist.Element(nameSpace + "recording");
                    if (recording == null)
                    {
                        QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from MusicBrainz";
                        ResultOfQuery = false;
                        return false;
                    }
                    string trackArtist = recording.Element(nameSpace + "artist-credit").Element(nameSpace + "name-credit").Element(nameSpace + "artist").Element(nameSpace + "name").Value.ToString();
                    string trackTitle = recording.Element(nameSpace + "title").Value.ToString();

                    var releases = from f in recording.Elements(nameSpace + "release-list").Elements(nameSpace + "release")
                                   select f;
                    if (releases.Any())
                    {
                        if (limit == 1)
                        {
                            foreach (var release in releases)
                            {
                                string trackAlbum = "";
                                if (release.Element(nameSpace + "album") != null)
                                {
                                    trackAlbum = release.Element(nameSpace + "album").Value.ToString();
                                }

                                RetrievedMetadata = new Id3Tag
                                {
                                    Title = trackTitle,
                                    Artist = trackArtist,
                                    Album = trackAlbum,
                                };
                                ResultOfQuery = true;
                                return true;
                            }
                        }
                        else
                        {
                            int trackCount = 1;
                            foreach (var release in releases)
                            {
                                string trackAlbum = "";
                                if (release.Element(nameSpace + "album") != null)
                                {
                                    trackAlbum = release.Element(nameSpace + "album").Value.ToString();
                                }

                                RetrievedMetadata = new Id3Tag
                                {
                                    Title = trackTitle,
                                    Artist = trackArtist,
                                    Album = trackAlbum,
                                };
                                if (limit > 1)
                                    ListRetrievedTags.Add(RetrievedMetadata);
                                trackCount++;
                                if (trackCount > limit)
                                {
                                    break;
                                }
                            }

                            ResultOfQuery = true;
                            return true;
                        }
                    }
                }
                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from MusicBrainz";
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            return false;
        }

        public bool QueryForRelease(System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                string encodedArtist = System.Web.HttpUtility.UrlEncode(ExistingMetadata.Artist.TrimEnd());
                string query = query = Properties.Settings.Default.MBReleaseUrl + "artist:" + encodedArtist;
                string fullUserAgent = $"{Properties.Settings.Default.MBBaseUrl} {Properties.Settings.Default.MBBaseUrl}/{Properties.Settings.Default.MBVersion} ({Properties.Settings.Default.MBUserAgentUrl})";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);
                request.UserAgent = fullUserAgent;
                request.Accept = "application/xml";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string results = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (results != null)
                {
                    XElement root = XElement.Parse(results);
                    XNamespace nameSpace = root.Name.Namespace;
                    var releases = from f in root.Elements(nameSpace + "release-list").Elements(nameSpace + "release")
                                   select f;
                    if (releases.Any())
                    {
                        foreach (var release in releases)
                        {
                            string trackAlbum = "";
                            if (release.Element(nameSpace + "release-group") != null)
                            {
                                if (release.Element(nameSpace + "release-group").Element(nameSpace + "title") != null)
                                    trackAlbum = release.Element(nameSpace + "release-group").Element(nameSpace + "title").Value.ToString();
                            }

                            RetrievedMetadata = new Id3Tag
                            {
                                Album = trackAlbum,
                            };
                            ListRetrievedTags.Add(RetrievedMetadata);
                        }

                        ResultOfQuery = true;
                        return true;
                    }
                }
                QueryResult = "No Results for " + ExistingMetadata.Title + " - " + ExistingMetadata.Artist.TrimEnd() + " from MusicBrainz";
            }
            catch (Exception ex)
            {
                QueryResult = ex.Message;
            }

            return false;
        }

    }
}
