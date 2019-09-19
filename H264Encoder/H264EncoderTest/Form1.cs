using H264HardwareEncoderLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;

namespace EncoderH264
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            enc = new H264Encoder("192.168.1.168", "admin", "!3nc0d3r!");
        }

        public H264Encoder enc;

        Dictionary<string, Dictionary<string, string>> status;
        Dictionary<string, Dictionary<string, string>> output;
        Dictionary<string, Dictionary<string, string>> sys;
        Dictionary<string, Dictionary<string, string>> adv;
        Dictionary<string, Dictionary<string, string>> input;
        Dictionary<string, Dictionary<string, string>> osd;
        Dictionary<string, string> version;

        private Dictionary<string, string> selectedDic;

        public void GetStatus()
        {
            status = enc.LoadStatus();
            output = enc.LoadOutputs();
            sys = enc.LoadSys();
            adv = enc.LoadAdv();
            input = enc.LoadInputs();
            osd = enc.LoadOsd();
            version = enc.LoadVersion();


            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadStatus()["status"].ToArray();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadOutputs()["output0"].ToArray();
            selectedDic = enc.Output["output0"];
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadOutputs()["output1"].ToArray();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadOutputs()["output2"].ToArray();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadOutputs()["output3"].ToArray();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadSys()["sys"].ToArray();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadSys()["wifi0"].ToArray();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadSys()["wifi1"].ToArray();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadSys()["wifi2"].ToArray();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadAdv()["adv"].ToArray();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadOsd()["osd0"].ToArray();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadOsd()["osd1"].ToArray();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadOsd()["osd2"].ToArray();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadOsd()["osd3"].ToArray();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadInputs()["input"].ToArray();
        }
        private void button16_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadStatus()["g4"].ToArray();
        }
        private void button17_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadStatus()["wifi"].ToArray();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadStatus()["lan_dhcp"].ToArray();
        }
        private void button19_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadStatus()["vi"].ToArray();
        }
        private void button20_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadStatus()["venc0"].ToArray();
        }
        private void button21_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadStatus()["venc1"].ToArray();
        }
        private void button22_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadStatus()["venc2"].ToArray();
        }
        private void button23_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enc.LoadStatus()["venc3"].ToArray();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(((DataGridView)sender)[0, e.RowIndex].Value.ToString());
            //selectedDic[((DataGridView)sender)[0, e.RowIndex].Value.ToString()]

            //string input = ((DataGridView)sender)[1, e.RowIndex].Value.ToString();
            //ShowInputDialog(ref input);
            //selectedDic[((DataGridView)sender)[0,  e.RowIndex].Value.ToString()] = input;
        }

        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Name";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //enc.Sys["wifi1"]["wifi_essid"] = "";
            //enc.Sys["wifi1"]["wifi_psk"] = "";
            //enc.SetSysWifi(1);

            //enc.Sys["wifi2"]["wifi_essid"] = "";
            //enc.Sys["wifi2"]["wifi_psk"] = "";
            //enc.SetSysWifi(2);

            //MessageBox.Show("Test");

            //enc.Output["output0"]["rtmp_publish_uri"] = "rtmps://live-api-s.facebook.com:443/rtmp/2806723266023950?s_bl=1&amp;s_ps=1&amp;s_sml=3&amp;s_sw=0&amp;s_vt=api-s&amp;a=AbxlcdZ9ndZCiTDK";
            //enc.SetOutput(0);

            MessageBox.Show(enc.RtmpPublishStatus(0));
        }

        private void button25_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(enc.RtmpPublishStatus(1));
        }

        private void button26_Click(object sender, EventArgs e)
        {
            MessageBox.Show(enc.RtmpPublishStatus(2));
        }

        private void button27_Click(object sender, EventArgs e)
        {
            MessageBox.Show(enc.RtmpPublishStatus(3));
        }

        private void button28_Click(object sender, EventArgs e)
        {
            enc.SetValue(0, enumOutput.rtmp_publish_enable, "1");
            enc.SetOutput(0, enumOutput.rtmp_publish_enable);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            enc.SetValue(0, enumOutput.rtmp_publish_enable, "0");
            enc.SetOutput(0, enumOutput.rtmp_publish_enable);
        }

        private void button31_Click(object sender, EventArgs e)
        {
        }

        private void button30_Click(object sender, EventArgs e)
        {
        }

        private void button33_Click(object sender, EventArgs e)
        {
        }

        private void button32_Click(object sender, EventArgs e)
        {
        }

        private void button35_Click(object sender, EventArgs e)
        {
        }

        private void button34_Click(object sender, EventArgs e)
        {
        }

        private void button36_Click(object sender, EventArgs e)
        {
            enc.LoadStatus();
            enc.LoadSys();
            enc.LoadAdv();
            enc.LoadInputs();
            enc.LoadOutputs();
            enc.LoadOsd();
            enc.LoadVersion();

            enc.SetValue(0, enumSysWifi.wifi_essid, "SchafMediaProd");
            enc.SetValue(0, enumSysWifi.wifi_psk, "()!3nc0d3r!()");
            enc.SetValue(0, enumSysWifi.wifi_dhcp_enable, "1");
            enc.SetSysWifi(0, enumSysWifi.wifi_essid, enumSysWifi.wifi_psk, enumSysWifi.wifi_dhcp_enable);

            enc.SetValue(1, enumSysWifi.wifi_essid, "");
            enc.SetValue(1, enumSysWifi.wifi_psk, "");
            enc.SetValue(1, enumSysWifi.wifi_dhcp_enable, "0");
            enc.SetSysWifi(1, enumSysWifi.wifi_essid, enumSysWifi.wifi_psk, enumSysWifi.wifi_dhcp_enable);

            enc.SetValue(2, enumSysWifi.wifi_essid, "");
            enc.SetValue(2, enumSysWifi.wifi_psk, "");
            enc.SetValue(2, enumSysWifi.wifi_dhcp_enable, "0");
            enc.SetSysWifi(2, enumSysWifi.wifi_essid, enumSysWifi.wifi_psk, enumSysWifi.wifi_dhcp_enable);

            enc.SetValue(0, enumOutput.http_ts_enable, "0");
            enc.SetValue(0, enumOutput.http_hls_enable, "0");
            enc.SetValue(0, enumOutput.http_flv_enable, "0");
            enc.SetValue(0, enumOutput.rtsp_enable, "1");
            enc.SetValue(0, enumOutput.rtmp_enable, "0");
            enc.SetValue(0, enumOutput.multicast_enable, "0");
            enc.SetValue(0, enumOutput.rtmp_publish_enable, "0");
            enc.SetValue(0, enumOutput.rtmp_publish_uri, "rtmps://live-api-s.facebook.com:443/rtmp/2806723266023950?s_bl=1&s_ps=1&s_sml=3&s_sw=0&s_vt=api-s&a=AbxlcdZ9ndZCiTDK");
            enc.SetOutput(0, enumOutput.rtmp_publish_uri, enumOutput.http_ts_enable, enumOutput.http_hls_enable, enumOutput.http_flv_enable, enumOutput.rtsp_enable, enumOutput.rtmp_enable, enumOutput.multicast_enable, enumOutput.rtmp_publish_enable);

            enc.SetValue(0, enumOutput.venc_framerate, "25");
            enc.SetValue(0, enumOutput.venc_gop, "6");
            enc.SetOutput(0, enumOutput.venc_framerate, enumOutput.venc_gop);

            enc.SetValue(1, enumOutput.venc_framerate, "25");
            enc.SetValue(1, enumOutput.venc_gop, "6");
            enc.SetOutput(1, enumOutput.venc_framerate, enumOutput.venc_gop);

            enc.SetValue(2, enumOutput.venc_framerate, "25");
            enc.SetValue(2, enumOutput.venc_gop, "6");
            enc.SetOutput(2, enumOutput.venc_framerate, enumOutput.venc_gop);

            enc.SetValue(3, enumOutput.venc_framerate, "25");
            enc.SetValue(3, enumOutput.venc_gop, "6");
            enc.SetOutput(3, enumOutput.venc_framerate, enumOutput.venc_gop);

            enc.SetValue(0, enumOsd.enable, "1");
            enc.SetValue(0, enumOsd.type, textBox1.Text);
            enc.SetOsd(0, enumOsd.enable, enumOsd.type);
            

            enc.SetValue(enumAdv.ntp_enable, "1");
            enc.SetValue(enumAdv.ntp_server, "time.windows.com");
            enc.SetValue(enumAdv.time_zone, "2");
            enc.SetAdv(enumAdv.ntp_server, enumAdv.ntp_enable, enumAdv.time_zone);

            enc.SetValue(enumSys.html_password, "!3nc0d3r!");
            enc.SetValue(enumSys.old_password, "admin");
            enc.SetValue(enumSys.dhcp_enable, "1");
            enc.SetSys(enumSys.html_password, enumSys.old_password, enumSys.dhcp_enable);


        }
    }
}
