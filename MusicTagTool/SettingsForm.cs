using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalTagger
{
    public partial class SettingsForm : Form
    {
        private AppSettings settings = new AppSettings();

        private bool showDiscogs = false;
        private bool showGaliboo = false;
        private bool showGenius = false;
        private bool showLastFM = false;
        private bool showMusixMatch = false;
        private bool showNapter = false;
        private bool showSpotify = false;
        private bool showSpotifyID = false;
        private bool showDiscogsID = false;

        public SettingsForm(AppSettings serviceSettings)
        {
            InitializeComponent();
            settings = serviceSettings;
        }

        private void chkEnableSpotify_CheckedChanged(object sender, EventArgs e)
        {
            txtSpotifyClientID.Enabled = txtSpotifySecretClient.Enabled = btnShowSpotify.Enabled = chkEnableSpotify.Checked;
        }

        private void chkLastFM_CheckedChanged(object sender, EventArgs e)
        {
            txtLastFMApiKey.Enabled = btnShowLastFM.Enabled = chkLastFM.Checked;
        }

        private void chkEnableDiscogs_CheckedChanged(object sender, EventArgs e)
        {
            txtDiscogsConsumerKey.Enabled = txtDiscogsConsumerSecret.Enabled = btnShowDiscogs.Enabled = chkEnableDiscogs.Checked;
        }

        private void chkEnableNapster_CheckedChanged(object sender, EventArgs e)
        {
            txtNapsterAPIKey.Enabled = btnShowNapster.Enabled = chkEnableNapster.Checked;
        }

        private void chkEnableGaliboo_CheckedChanged(object sender, EventArgs e)
        {
            txtGalibooApiKey.Enabled = btnShowGaliboo.Enabled = chkEnableGaliboo.Checked;
        }

        private void chkEnableGenius_CheckedChanged(object sender, EventArgs e)
        {
            txtGeniusAccessToken.Enabled =  btnShowGenius.Enabled = chkEnableGenius.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string clientID = "";
            string clientKey = "";

            if( chkEnableDiscogs.Checked)
            {
                clientID = txtDiscogsConsumerKey.Text;
                clientKey = txtDiscogsConsumerSecret.Text;

                if( String.IsNullOrEmpty(clientID) || String.IsNullOrEmpty(clientKey))
                {
                    return;
                }

                ConfigData.SetConfigData("DCCI", ConfigData.EncryptString(clientID, Properties.Resources.Pass+Properties.Resources.Phrase));
                ConfigData.SetConfigData("DCCK", ConfigData.EncryptString(clientKey, Properties.Resources.Pass+Properties.Resources.Phrase));
            }
            else
            {
                ConfigData.SetConfigData("DCCI", "");
                ConfigData.SetConfigData("DCCK", "");
            }

            if ( chkEnableGaliboo.Checked)
            {
                clientKey = txtGalibooApiKey.Text;
                if (String.IsNullOrEmpty(clientKey))
                {
                    return;
                }

                ConfigData.SetConfigData("GACK", ConfigData.EncryptString(clientKey, Properties.Resources.Pass+Properties.Resources.Phrase));
            }
            else
            {
                ConfigData.SetConfigData("GACK", "");
            }

            if ( chkEnableGenius.Checked)
            {
                clientKey = txtGeniusAccessToken.Text;
                if (String.IsNullOrEmpty(clientKey))
                {
                    return;
                }
                ConfigData.SetConfigData("GECK", ConfigData.EncryptString(clientKey, Properties.Resources.Pass+Properties.Resources.Phrase));
            }
            else
            {
                ConfigData.SetConfigData("GECK", "");
            }

            if ( chkLastFM.Checked)
            {
                clientKey = txtLastFMApiKey.Text;
                if (String.IsNullOrEmpty(clientKey))
                {
                    return;
                }
                ConfigData.SetConfigData("LFCK", ConfigData.EncryptString(clientKey, Properties.Resources.Pass+Properties.Resources.Phrase));
            }
            else
            {
                ConfigData.SetConfigData("LFCK", "");
            }

            if (chkEnableMusixMatch.Checked)
            {
                clientKey = txtMusixMatchKey.Text;
                if (String.IsNullOrEmpty(clientKey))
                {
                    return;
                }
                ConfigData.SetConfigData("MXCK", ConfigData.EncryptString(clientKey, Properties.Resources.Pass+Properties.Resources.Phrase));
            }
            else
            {
                ConfigData.SetConfigData("MXCK", "");
            }

            if ( chkEnableNapster.Checked)
            {
                clientKey = txtNapsterAPIKey.Text;
                if (String.IsNullOrEmpty(clientKey))
                {
                    return;
                }
                ConfigData.SetConfigData("NACK", ConfigData.EncryptString(clientKey, Properties.Resources.Pass+Properties.Resources.Phrase));
            }
            else
            {
                ConfigData.SetConfigData("NACK", "");
            }

            if (chkEnableSpotify.Checked)
            {
                clientID = txtSpotifyClientID.Text;
                clientKey = txtSpotifySecretClient.Text;
                if (String.IsNullOrEmpty(clientID) || String.IsNullOrEmpty(clientKey))
                {
                    return;
                }
                ConfigData.SetConfigData("SPCI", ConfigData.EncryptString(clientID, Properties.Resources.Pass + Properties.Resources.Phrase));
                ConfigData.SetConfigData("SPCK", ConfigData.EncryptString(clientKey, Properties.Resources.Pass+Properties.Resources.Phrase));
            }
            else
            {
                ConfigData.SetConfigData("SPCI", "");
                ConfigData.SetConfigData("SPCK", "");
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void chkEnableMusixMatch_CheckedChanged(object sender, EventArgs e)
        {
            txtMusixMatchKey.Enabled =  btnShowMusixMatch.Enabled = chkEnableMusixMatch.Checked;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            chkEnableDiscogs.Checked = !String.IsNullOrEmpty(settings.DiscogsClientID);
            txtDiscogsConsumerKey.Enabled = txtDiscogsConsumerSecret.Enabled = btnShowDiscogs.Enabled = chkEnableDiscogs.Checked;
            txtDiscogsConsumerKey.Text = settings.DiscogsClientID;
            txtDiscogsConsumerSecret.Text = settings.DiscogsClientKey;
            
            chkEnableGaliboo.Checked = !String.IsNullOrEmpty(settings.GalibooClientKey);
            txtGalibooApiKey.Enabled = btnShowGaliboo.Enabled = chkEnableGaliboo.Checked;
            txtGalibooApiKey.Text = settings.GalibooClientKey;

            chkEnableGenius.Checked = !String.IsNullOrEmpty(settings.GeniusClientKey);
            txtGeniusAccessToken.Enabled = btnShowGenius.Enabled = chkEnableGenius.Checked;
            txtGeniusAccessToken.Text = settings.GeniusClientKey;

            chkLastFM.Checked = !String.IsNullOrEmpty(settings.LastFMClientKey);
            txtLastFMApiKey.Enabled = btnShowLastFM.Enabled = chkLastFM.Checked;
            txtLastFMApiKey.Text = settings.LastFMClientKey;

            chkEnableMusixMatch.Checked = !String.IsNullOrEmpty(settings.MusixMatchKey);
            txtMusixMatchKey.Enabled = btnShowMusixMatch.Enabled = chkEnableMusixMatch.Checked;
            txtMusixMatchKey.Text = settings.MusixMatchKey;

            chkEnableNapster.Checked = !String.IsNullOrEmpty(settings.NapsterClientKey);
            txtNapsterAPIKey.Enabled = btnShowNapster.Enabled = chkEnableNapster.Checked;
            txtNapsterAPIKey.Text = settings.NapsterClientKey;

            chkEnableSpotify.Checked = !String.IsNullOrEmpty(settings.SpotifyClientID);
            txtSpotifyClientID.Enabled = txtSpotifySecretClient.Enabled = btnShowSpotify.Enabled = chkEnableSpotify.Checked;
            txtSpotifyClientID.Text = settings.SpotifyClientID;
            txtSpotifySecretClient.Text = settings.SpotifyClientKey;
        }

        private void btnShowDiscogs_Click(object sender, EventArgs e)
        {
            showDiscogs = ShowOrHideKey(showDiscogs, txtDiscogsConsumerSecret, btnShowDiscogs);
        }

        private void btnShowGaliboo_Click(object sender, EventArgs e)
        {
            showGaliboo = ShowOrHideKey(showGaliboo, txtGalibooApiKey, btnShowGaliboo);
        }

        private void btnShowGenius_Click(object sender, EventArgs e)
        {
            showGenius = ShowOrHideKey(showGenius, txtGeniusAccessToken, btnShowGenius);
        }

        private void btnShowLastFM_Click(object sender, EventArgs e)
        {
            showLastFM = ShowOrHideKey(showLastFM, txtLastFMApiKey, btnShowLastFM);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showMusixMatch = ShowOrHideKey(showMusixMatch, txtMusixMatchKey, btnShowMusixMatch);
        }

        private void btnShowNapster_Click(object sender, EventArgs e)
        {
            showNapter = ShowOrHideKey(showNapter, txtNapsterAPIKey, btnShowNapster);
        }

        private void btnShowSpotify_Click(object sender, EventArgs e)
        {
            showSpotify = ShowOrHideKey(showSpotify, txtSpotifySecretClient, btnShowSpotify);
        }

        private bool ShowOrHideKey(bool showValue, TextBox txtKey, Button btnShow)
        {
            if (showValue)
            {
                txtKey.PasswordChar = '*';
                showValue = false;
                btnShow.BackColor = SystemColors.Control;
            }
            else
            {
                txtKey.PasswordChar = '\0';
                showValue = true;
                btnShow.BackColor = Color.Yellow;
            }

            return showValue;
        }

        private void btnShowDiscogsID_Click(object sender, EventArgs e)
        {
            showDiscogsID = ShowOrHideKey(showDiscogsID, txtDiscogsConsumerKey, btnShowDiscogsID);
        }

        private void btnShowSpotifyID_Click(object sender, EventArgs e)
        {
            showSpotifyID = ShowOrHideKey(showSpotifyID, txtSpotifySecretClient, btnShowSpotifyID);
        }
    }
}
