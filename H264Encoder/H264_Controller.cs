#region License
/* ======================================================================
     Copyright (c) 2017 - Schaftenaar Media Productions
                          www.schaftenaarmediaproductions.com
                          info@schaftenaarmediaproductions.com

     Permission is hereby granted, free of charge, to any person
     obtaining a copy of this software and associated documentation
     files (the "Software"), to deal in the Software without
     restriction, including without limitation the rights to use,
     copy, modify, merge, publish, distribute, sublicense, and/or sell
     copies of the Software, and to permit persons to whom the
     Software is furnished to do so, subject to the following
     conditions:

     The above copyright notice and this permission notice shall be
     included in all copies or substantial portions of the Software.

     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
     EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
     OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
     NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
     HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
     OTHER DEALINGS IN THE SOFTWARE.
   ======================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.IO;

namespace H264HardwareEncoderLib
{
    public enum enumOsd
    {
        enable,
        type,
        x,
        y,
        alpha,
        font_size,
        color,
        bcolor,
        txt,
        bmp
    }

    public enum enumVenc
    {
        left_pics,
        left_stream_bytes,
        left_stream_frame,
        packs,
        enable,
        codec,
        width,
        height,
        framerate,
        bitrate,
        mjpg_url,
        rtmp_publish_url,
        rtmp_status,
        ts_url0,
        jpg_url0,
        mjpg_url0,
        hls_url0,
        flv_url0,
        rtsp_url0,
        rtmp_url0,
        multicast_url0
    }

    public enum enumAdv
    {
        interlaced_only_bottom,
        field_to_frame,
        ts_muxer,
        ts_once,
        httpts_password_enable,
        g4_gw_as_dns,
        ntp_server,
        ntp_enable,
        time_zone,
        hls_buffer_number,
        hls_splitter_time,
        ts_transport_stream_id,
        ts_pmt_start_pid,
        ts_start_pid,
        ts_tables_version,
        ts_rc_mode,
        ts_service_name,
        ts_service_provider,
        vmix_compatible,
        audio_only,
        video_only,
        auto_super_frame_reencode,
        slice_split_enable,
        slice_split_size,
        min_qp,
        max_qp,
        i_qp,
        p_qp,
        schedule_restart_enable,
        schedule_restart_time,
        net_packet_drop_threshold,
        remserial_baudrate,
        remserial_tcp_port,
        csc_enable,
        csc_contrast
    }

    public enum enumSysWifi
    {
        wifi_essid,
        wifi_psk,
        wifi_ip,
        wifi_netmask,
        wifi_gateway,
        wifi_dhcp_enable,
        wifi_index
    }

    public enum enumOutput
    {
        aenc_codec,
        aenc_bitrate,
        venc_enable,
        venc_codec,
        venc_gop,
        venc_width_height_same_as_input,
        venc_width,
        venc_height,
        venc_framerate,
        venc_profile,
        venc_rc_mode,
        venc_bitrate,
        http_private_enable,
        http_private_uri,
        http_ts_enable,
        http_ts_uri,
        http_jpg_enable,
        http_jpg_uri,
        http_mjpg_enable,
        http_mjpg_uri,
        http_hls_enable,
        http_hls_uri,
        http_flv_enable,
        http_flv_uri,
        rtsp_enable,
        rtsp_uri,
        rtmp_enable,
        rtmp_uri,
        rtmp_publish_enable,
        rtmp_publish_uri,
        multicast_enable,
        multicast_ip,
        multicast_port,
        unicast_enable,
        unicast_port
    }

    public enum enumSys
    {
        ip,
        netmask,
        gateway,
        mac,
        dhcp_enable,
        wifi_enable,
        wifi_ap_mode,
        wifi_hostap_hw_mode,
        wifi_hostap_essid,
        wifi_hostap_psk,
        wifi_hostap_channel,
        dns0,
        dns1,
        http_port,
        rtsp_port,
        rtsp_g711,
        rtsp_g711_8k,
        rtsp_g711_mu,
        audio_left_right,
        pte_g711,
        ts_over_rtsp,
        rtp_multicast,
        udp_ttl,
        udp_sock_buf_size,
        html_password,
        old_password,
        hostname,
        language
    }

    public enum encCodecs
    {
        H264 = 96,
        H265 = 265,
        MJPEG = 1002
    }

    public enum encVencProfile
    {
        baseline,
        main,
        high
    }

    public enum encVencRcMode
    {
        cvr,
        vbr
    }

    public class H264Encoder
    {
        private string EncoderIP;
        private string EncoderUsername;
        private string EncoderPassword;
        private string EncoderResponse;

        public Dictionary<string, string> Version;

        public Dictionary<string, Dictionary<string, string>> Status;
        private Dictionary<string, string> status_stat;
        private Dictionary<string, string> status_g4;
        private Dictionary<string, string> status_wifi;
        private Dictionary<string, string> status_lan_dhcp;
        private Dictionary<string, string> status_vi;
        private Dictionary<string, string> status_venc0;
        private Dictionary<string, string> status_venc1;
        private Dictionary<string, string> status_venc2;
        private Dictionary<string, string> status_venc3;
        private Dictionary<string, string> status_user;

        public Dictionary<string, Dictionary<string, string>> Sys;
        private Dictionary<string, string> sys_sys;
        private Dictionary<string, string> sys_wifi0;
        private Dictionary<string, string> sys_wifi1;
        private Dictionary<string, string> sys_wifi2;

        public Dictionary<string, Dictionary<string, string>> Adv;
        private Dictionary<string, string> adv_adv;

        public Dictionary<string, Dictionary<string, string>> Input;
        private Dictionary<string, string> input_input;

        public Dictionary<string, Dictionary<string, string>> Output;
        private Dictionary<string, string> output0;
        private Dictionary<string, string> output1;
        private Dictionary<string, string> output2;
        private Dictionary<string, string> output3;

        public Dictionary<string, Dictionary<string, string>> Osd;
        private Dictionary<string, string> osd0;
        private Dictionary<string, string> osd1;
        private Dictionary<string, string> osd2;
        private Dictionary<string, string> osd3;

        public H264Encoder(string ip, string usr, string pwd)
        {
            this.EncoderIP = ip;
            this.EncoderUsername = usr;
            this.EncoderPassword = pwd;
        }
        public string GetData(string url)
        {
            WebRequest request = WebRequest.Create(string.Format("http://{0}{1}", this.EncoderIP, url));
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(this.EncoderUsername + ":" + this.EncoderPassword));
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response;

            try
            {
                response = request.GetResponse();
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        try
                        {
                            this.EncoderResponse = new StreamReader(dataStream).ReadToEnd();
                        }
                        finally
                        { }
                    }
                }
                else
                {
                    this.EncoderResponse = "";
                }
                response.Close();
                response.Dispose();
            }
            catch (Exception)
            {
            }
            return this.EncoderResponse;
        }
        public string SetData(string url)
        {
            WebRequest request = WebRequest.Create(string.Format("http://{0}{1}", this.EncoderIP, url));
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(EncoderUsername + ":" + EncoderPassword));
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response;
            string StatusDescription;

            try
            {
                response = request.GetResponse();
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    StatusDescription=((HttpWebResponse)response).StatusDescription;
                    response.Dispose();
                    return StatusDescription;
                }
            }
            catch (Exception)
            {
            }
            return "";
        }

        public Dictionary<string, string> LoadVersion()
        {
            this.Version = new Dictionary<string, string>();

            var doc = new XmlDocument();

            try
            {
                doc.LoadXml(this.GetData("/get_version"));

                Debug.WriteLine("[VERSION]");
                Debug.WriteLine(string.Format("{0} = {1}", "version", doc.InnerText.Trim()));
                this.Version.Add("version", doc.InnerText.Trim());
                Debug.WriteLine("================================================================================");

                return Version;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        public Dictionary<string, Dictionary<string, string>> LoadStatus()
        {
            this.status_stat = new Dictionary<string, string>();
            this.status_g4 = new Dictionary<string, string>();
            this.status_wifi = new Dictionary<string, string>();
            this.status_lan_dhcp = new Dictionary<string, string>();
            this.status_vi = new Dictionary<string, string>();
            this.status_venc0 = new Dictionary<string, string>();
            this.status_venc1 = new Dictionary<string, string>();
            this.status_venc2 = new Dictionary<string, string>();
            this.status_venc3 = new Dictionary<string, string>();
            this.status_user = new Dictionary<string, string>();

            var doc = new XmlDocument();

            try
            {
                doc.LoadXml(this.GetData("/get_status"));

                Debug.WriteLine("[STATUS]");
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("status")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.status_stat.Add(item.Name, item.InnerXml); this.status_stat.Remove("g4"); this.status_stat.Remove("lan_dhcp"); } }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("g4")[0].ChildNodes) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.status_g4.Add(item.Name, item.InnerXml); }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("wifi")[0].ChildNodes) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.status_wifi.Add(item.Name, item.InnerXml); }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("lan_dhcp")[0].ChildNodes) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.status_lan_dhcp.Add(item.Name, item.InnerXml); }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("vi")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.status_vi.Add(item.Name, item.InnerXml); } }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("venc")[0].ChildNodes) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.status_venc0.Add(item.Name, item.InnerXml); }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("venc")[1].ChildNodes) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.status_venc1.Add(item.Name, item.InnerXml); }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("venc")[2].ChildNodes) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.status_venc2.Add(item.Name, item.InnerXml); }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("venc")[3].ChildNodes) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.status_venc3.Add(item.Name, item.InnerXml); }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("user")[0].ChildNodes) { status_user.Add(item.Name, item.InnerXml); }
                Debug.WriteLine("================================================================================");

                this.Status = new Dictionary<string, Dictionary<string, string>>
            {
                { "status", this.status_stat },
                { "g4", this.status_g4 },
                { "wifi", this.status_wifi },
                { "lan_dhcp", this.status_lan_dhcp },
                { "vi", this.status_vi },
                { "venc0", this.status_venc0 },
                { "venc1", this.status_venc1 },
                { "venc2", this.status_venc2 },
                { "venc3", this.status_venc3 },
                { "user", this.status_user }
            };
                return this.Status;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        public Dictionary<string, Dictionary<string, string>> LoadSys()
        {
            this.sys_sys = new Dictionary<string, string>();
            this.sys_wifi0 = new Dictionary<string, string>();
            this.sys_wifi1 = new Dictionary<string, string>();
            this.sys_wifi2 = new Dictionary<string, string>();

            var doc = new XmlDocument();

            try
            {
                doc.LoadXml(this.GetData("/get_sys"));

                Debug.WriteLine("[SYS]");
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("sys")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.sys_sys.Add(item.Name, item.InnerXml); this.sys_sys.Remove("g4"); } }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("wifi")[0].ChildNodes) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.sys_wifi0.Add(item.Name, item.InnerXml); }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("wifi")[1].ChildNodes) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.sys_wifi1.Add(item.Name, item.InnerXml); }
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("wifi")[2].ChildNodes) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.sys_wifi2.Add(item.Name, item.InnerXml); }
                Debug.WriteLine("================================================================================");

                this.Sys = new Dictionary<string, Dictionary<string, string>>
            {
                { "sys", this.sys_sys },
                { "wifi0", this.sys_wifi0 },
                { "wifi1", this.sys_wifi1 },
                { "wifi2", this.sys_wifi2 },
            };
                return Sys;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        public Dictionary<string, Dictionary<string, string>> LoadAdv()
        {
            this.adv_adv = new Dictionary<string, string>();

            var doc = new XmlDocument();

            try
            {
                doc.LoadXml(this.GetData("/get_adv"));
                Debug.WriteLine("[ADV]");
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("adv")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.adv_adv.Add(item.Name, item.InnerXml); } }
                Debug.WriteLine("================================================================================");

                this.Adv = new Dictionary<string, Dictionary<string, string>>
            {
                { "adv", this.adv_adv }
            };
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return Adv;
        }
        public Dictionary<string, Dictionary<string, string>> LoadInputs()
        {
            this.input_input = new Dictionary<string, string>();

            var doc = new XmlDocument();

            try
            {
                doc.LoadXml(this.GetData("/get_input"));
                Debug.WriteLine("[INPUT]");
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("input")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.input_input.Add(item.Name, item.InnerXml); } }
                Debug.WriteLine("================================================================================");

                this.Input = new Dictionary<string, Dictionary<string, string>>
            {
                { "input", this.input_input}
            };
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return Input; ;
        }
        public Dictionary<string, Dictionary<string, string>> LoadOutputs()
        {
            this.output0 = new Dictionary<string, string>();
            this.output1 = new Dictionary<string, string>();
            this.output2 = new Dictionary<string, string>();
            this.output3 = new Dictionary<string, string>();

            var doc = new XmlDocument();

            try
            {
                Debug.WriteLine("[OUTPUT]");
                doc.LoadXml(GetData("/get_output?input=0&output=0"));
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("output")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.output0.Add(item.Name, item.InnerXml); } }
                doc.LoadXml(GetData("/get_output?input=0&output=1"));
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("output")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.output1.Add(item.Name, item.InnerXml); } }
                doc.LoadXml(GetData("/get_output?input=0&output=2"));
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("output")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.output2.Add(item.Name, item.InnerXml); } }
                doc.LoadXml(GetData("/get_output?input=0&output=3"));
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("output")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.output3.Add(item.Name, item.InnerXml); } }
                Debug.WriteLine("================================================================================");

                this.Output = new Dictionary<string, Dictionary<string, string>>
            {
                { "output0", this.output0 },
                { "output1", this.output1 },
                { "output2", this.output2 },
                { "output3", this.output3 }
            };
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return Output;
        }
        public Dictionary<string, Dictionary<string, string>> LoadOsd()
        {
            this.osd0 = new Dictionary<string, string>();
            this.osd1 = new Dictionary<string, string>();
            this.osd2 = new Dictionary<string, string>();
            this.osd3 = new Dictionary<string, string>();

            var doc = new XmlDocument();

            try
            {
                Debug.WriteLine("[OSD]");
                doc.LoadXml(GetData("/get_osd?enc_chn=0&osd_chn=0"));
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("osd")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.osd0.Add(item.Name, item.InnerXml); } }
                doc.LoadXml(GetData("/get_osd?enc_chn=0&osd_chn=1"));
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("osd")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.osd1.Add(item.Name, item.InnerXml); } }
                doc.LoadXml(GetData("/get_osd?enc_chn=0&osd_chn=2"));
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("osd")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.osd2.Add(item.Name, item.InnerXml); } }
                doc.LoadXml(GetData("/get_osd?enc_chn=0&osd_chn=3"));
                foreach (XmlLinkedNode item in doc.GetElementsByTagName("osd")[0].ChildNodes) { if (item.ChildNodes.Count == 1) { Debug.WriteLine(string.Format("{0} = {1}", item.Name, item.InnerXml)); this.osd3.Add(item.Name, item.InnerXml); } }
                Debug.WriteLine("================================================================================");

                this.Osd = new Dictionary<string, Dictionary<string, string>>
            {
                { "osd0", this.osd0 },
                { "osd1", this.osd1 },
                { "osd2", this.osd2 },
                { "osd3", this.osd3 }
            };
                return Osd;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public bool SetValue(object d, string v)
        {
            Dictionary<string, string> dic = null;

            try
            {
                switch (d.GetType().Name)
                {
                    case "enumSys":
                        dic = this.Sys["sys"];
                        break;
                    case "enumAdv":
                        dic = this.Adv["adv"];
                        break;

                    default:
                        break;
                }
                dic[d.ToString()] = v;
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
        public bool SetValue(int n, object d, string v)
        {
            Dictionary<string, string> dic = null;

            try
            {
                switch (d.GetType().Name)
                {
                    case "enumOutput":
                        dic = this.Output["output" + n];
                        break;
                    case "enumSysWifi":
                        dic = this.Sys["wifi" + n];
                        break;
                    case "enumOsd":
                        dic = this.Osd["osd" + n];
                        break;
                    default:
                        break;
                }
                dic[d.ToString()] = v;
                return true;
            }
            catch(NullReferenceException)
            {
                return false;
            }
        }

        public object GetSys(enumSys k, bool online)
        {
            if (this.Sys != null)
            {
                object obj = null;
                Dictionary<string, string> dic = online == true ? this.LoadSys()["sys"] : this.Sys["sys"];

                switch (k)
                {
                    case enumSys.ip:
                    case enumSys.netmask:
                    case enumSys.gateway:
                    case enumSys.mac:
                    case enumSys.dhcp_enable:
                    case enumSys.wifi_enable:
                    case enumSys.wifi_ap_mode:
                    case enumSys.wifi_hostap_hw_mode:
                    case enumSys.wifi_hostap_essid:
                    case enumSys.wifi_hostap_psk:
                    case enumSys.wifi_hostap_channel:
                    case enumSys.dns0:
                    case enumSys.dns1:
                    case enumSys.http_port:
                    case enumSys.rtsp_port:
                    case enumSys.rtsp_g711:
                    case enumSys.rtsp_g711_8k:
                    case enumSys.rtsp_g711_mu:
                    case enumSys.audio_left_right:
                    case enumSys.pte_g711:
                    case enumSys.ts_over_rtsp:
                    case enumSys.rtp_multicast:
                    case enumSys.udp_ttl:
                    case enumSys.udp_sock_buf_size:
                    case enumSys.html_password:
                    case enumSys.hostname:
                    case enumSys.language:
                        obj = dic[k.ToString()];
                        break;
                    default:
                        break;
                }
                return obj;
            }
            return null;
        }
        public object GetSys(enumSys k)
        {
            return this.GetSys(k, false);
        }
        public string SetSys(params enumSys[] sysparams)
        {
            if (this.Sys != null)
            {
                Dictionary<string, string> dic = this.Sys["sys"];
                string url = "/set_sys?";

                foreach (var sp in sysparams)
                {
                    switch (sp)
                    {
                        case enumSys.ip:
                        case enumSys.netmask:
                        case enumSys.gateway:
                        case enumSys.mac:
                        case enumSys.wifi_hostap_essid:
                        case enumSys.dns0:
                        case enumSys.dns1:
                        case enumSys.wifi_hostap_psk:
                        case enumSys.html_password:
                        case enumSys.old_password:
                        case enumSys.hostname:
                        case enumSys.language:
                            url += "&" + sp.ToString() + "=" + WebUtility.UrlEncode(dic[sp.ToString()]);
                            break;
                        case enumSys.dhcp_enable:
                        case enumSys.wifi_enable:
                        case enumSys.wifi_ap_mode:
                        case enumSys.wifi_hostap_hw_mode:
                        case enumSys.http_port:
                        case enumSys.rtsp_port:
                        case enumSys.rtsp_g711:
                        case enumSys.rtsp_g711_8k:
                        case enumSys.rtsp_g711_mu:
                        case enumSys.audio_left_right:
                        case enumSys.pte_g711:
                        case enumSys.ts_over_rtsp:
                        case enumSys.rtp_multicast:
                        case enumSys.udp_ttl:
                        case enumSys.udp_sock_buf_size:
                        case enumSys.wifi_hostap_channel:
                            url += "&" + sp.ToString() + "=" + dic[sp.ToString()];
                            break;
                        default:
                            break;
                    }
                }
                url = url.Remove(url.IndexOf('&', 0), 1);
                this.SetData(url);
                return url;
            }
            return "";
        }

        public object GetSysWifi(int w, enumSysWifi k, bool online)
        {
            if (this.Sys != null)
            {
                object obj = null;
                Dictionary<string, string> dic = online == true ? this.LoadSys()["wifi" + w] : this.Sys["wifi" + w];

                switch (k)
                {
                    case enumSysWifi.wifi_essid:
                    case enumSysWifi.wifi_psk:
                    case enumSysWifi.wifi_ip:
                    case enumSysWifi.wifi_netmask:
                    case enumSysWifi.wifi_gateway:
                    case enumSysWifi.wifi_dhcp_enable:
                        //case enumSysWifi.wifi_index:
                        obj = dic[k.ToString()];
                        break;
                    default:
                        break;
                }
                return obj;
            }
            return null;
        }
        public object GetSysWifi(int w, enumSysWifi k)
        {
            return this.GetSysWifi(w, k, false);
        }
        public string SetSysWifi(int w, params enumSysWifi[] syswifiparams)
        {
            if (this.Sys != null)
            {
                Dictionary<string, string> dic = this.Sys["wifi" + w];
                string url = "/set_sys?";

                foreach (var sp in syswifiparams)
                {
                    switch (sp)
                    {
                        case enumSysWifi.wifi_essid:
                        case enumSysWifi.wifi_psk:
                            url += "&" + sp.ToString() + "=" + WebUtility.UrlEncode(WebUtility.HtmlDecode(dic[sp.ToString()]));
                            break;
                        case enumSysWifi.wifi_ip:
                        case enumSysWifi.wifi_netmask:
                        case enumSysWifi.wifi_gateway:
                            url += "&" + sp.ToString() + "=" + WebUtility.HtmlEncode("\"") + dic[sp.ToString()] + WebUtility.HtmlEncode("\"");
                            break;
                        case enumSysWifi.wifi_dhcp_enable:
                        case enumSysWifi.wifi_index:
                            url += "&" + sp.ToString() + "=" + dic[sp.ToString()];
                            break;
                        default:
                            break;
                    }
                }

                string essids = "";
                essids += this.GetSysWifi(0, enumSysWifi.wifi_essid).ToString().Trim();
                essids += this.GetSysWifi(1, enumSysWifi.wifi_essid).ToString().Trim();
                essids += this.GetSysWifi(2, enumSysWifi.wifi_essid).ToString().Trim();

                url += "&wifi_index=" + w;

                this.SetValue(enumSys.wifi_ap_mode, essids == "" ? "1" : "0");
                this.SetValue(w, enumSys.wifi_enable, essids == "" ? "0" : "1");
                this.SetSys(enumSys.wifi_ap_mode, enumSys.wifi_enable);

                url = url.Remove(url.IndexOf('&', 0), 1);
                this.SetData(url);
                return url;
            }
            return "";
        }

        public object GetOutput(int o, enumOutput k, bool online)
        {
            if (this.Output != null)
            {
                object obj = null;
                Dictionary<string, string> dic = online == true ? this.LoadOutputs()["output" + o] : this.Output["output" + o];

                switch (k)
                {
                    case enumOutput.aenc_codec:
                    case enumOutput.aenc_bitrate:
                    case enumOutput.venc_enable:
                    case enumOutput.venc_codec:
                    case enumOutput.venc_gop:
                    case enumOutput.venc_width_height_same_as_input:
                    case enumOutput.venc_width:
                    case enumOutput.venc_height:
                    case enumOutput.venc_framerate:
                    case enumOutput.venc_profile:
                    case enumOutput.venc_rc_mode:
                    case enumOutput.venc_bitrate:
                    case enumOutput.http_private_enable:
                    case enumOutput.http_private_uri:
                    case enumOutput.http_ts_enable:
                    case enumOutput.http_ts_uri:
                    case enumOutput.http_jpg_enable:
                    case enumOutput.http_jpg_uri:
                    case enumOutput.http_mjpg_enable:
                    case enumOutput.http_mjpg_uri:
                    case enumOutput.http_hls_enable:
                    case enumOutput.http_hls_uri:
                    case enumOutput.http_flv_enable:
                    case enumOutput.http_flv_uri:
                    case enumOutput.rtsp_enable:
                    case enumOutput.rtsp_uri:
                    case enumOutput.rtmp_enable:
                    case enumOutput.rtmp_uri:
                    case enumOutput.rtmp_publish_enable:
                    case enumOutput.rtmp_publish_uri:
                    case enumOutput.multicast_enable:
                    case enumOutput.multicast_ip:
                    case enumOutput.multicast_port:
                    case enumOutput.unicast_enable:
                    case enumOutput.unicast_port:
                        obj = dic[k.ToString()];
                        break;
                    default:
                        break;
                }
                return obj;
            }
            return null;
        }
        public object GetOutput(int o, enumOutput k)
        {
            return this.GetOutput(o, k, false);
        }
        public string SetOutput(int o, params enumOutput[] outputparms)
        {
            if (this.Output != null)
            {
                Dictionary<string, string> dic = this.Output["output" + o];
                string url = "/set_output?input=0&output=" + o;

                foreach (var sp in outputparms)
                {
                    switch (sp)
                    {
                        case enumOutput.venc_width_height_same_as_input:
                            url += "&" + sp.ToString() + "=" + (((dic[ "vi_cap_width"] == dic["venc_width"]) && (dic["vi_cap_height"] == dic["venc_height"])) ? 1 : 0).ToString();
                            break;
                        case enumOutput.aenc_codec:
                        case enumOutput.aenc_bitrate:
                        case enumOutput.venc_enable:
                        case enumOutput.venc_codec:
                        case enumOutput.venc_gop:
                        case enumOutput.venc_width:
                        case enumOutput.venc_height:
                        case enumOutput.venc_framerate:
                        case enumOutput.venc_profile:
                        case enumOutput.venc_rc_mode:
                        case enumOutput.venc_bitrate:
                        case enumOutput.http_private_enable:
                        case enumOutput.http_ts_enable:
                        case enumOutput.http_jpg_enable:
                        case enumOutput.http_mjpg_enable:
                        case enumOutput.http_hls_enable:
                        case enumOutput.http_flv_enable:
                        case enumOutput.rtsp_enable:
                        case enumOutput.rtmp_enable:
                        case enumOutput.rtmp_publish_enable:
                        case enumOutput.multicast_enable:
                        case enumOutput.multicast_port:
                        case enumOutput.unicast_enable:
                        case enumOutput.unicast_port:
                            url += "&" + sp.ToString() + "=" + dic[sp.ToString()];
                            break;
                        case enumOutput.http_private_uri:
                        case enumOutput.http_ts_uri:
                        case enumOutput.http_jpg_uri:
                        case enumOutput.http_mjpg_uri:
                        case enumOutput.http_hls_uri:
                        case enumOutput.http_flv_uri:
                        case enumOutput.rtsp_uri:
                        case enumOutput.rtmp_uri:
                        case enumOutput.multicast_ip:
                        case enumOutput.rtmp_publish_uri:
                            url += "&" + sp.ToString() + "=" + WebUtility.UrlEncode(dic[sp.ToString()]);
                            break;
                        default:
                            break;
                    }
                }
                this.SetData(url);
                return url;
            }
            return "";
        }

        public object GetOsd(int o, enumOsd k, bool online)
        {
            if (this.Osd != null)
            {
                object obj = null;
                Dictionary<string, string> dic = online == true ? this.LoadOsd()["osd" + o] : this.Osd["osd" + o];

                switch (k)
                {
                    case enumOsd.enable:
                    case enumOsd.type:
                    case enumOsd.x:
                    case enumOsd.y:
                    case enumOsd.alpha:
                    case enumOsd.font_size:
                    case enumOsd.color:
                    case enumOsd.bcolor:
                    case enumOsd.txt:
                    case enumOsd.bmp:
                        obj = dic[k.ToString()];
                        break;
                    default:
                        break;
                }
                return obj;
            }
            return null;
        }
        public object GetOsd(int o, enumOsd k)
        {
            return this.GetOsd(o, k, false);
        }
        public string SetOsd(int o, params enumOsd[] osdparms)
        {
            if (this.Osd != null)
            {
                Dictionary<string, string> dic = this.Osd["osd" + o];
                string url = "/set_osd?enc_chn=0&osd_chn=" + o;

                foreach (var sp in osdparms)
                {
                    switch (sp)
                    {
                        case enumOsd.txt:
                        case enumOsd.bmp:
                            url += "&" + sp.ToString() + "=" + WebUtility.UrlEncode(dic[sp.ToString()]);
                            break;
                        case enumOsd.enable:
                        case enumOsd.type:
                        case enumOsd.x:
                        case enumOsd.y:
                        case enumOsd.alpha:
                        case enumOsd.font_size:
                        case enumOsd.color:
                        case enumOsd.bcolor:
                            url += "&" + sp.ToString() + "=" + dic[sp.ToString()];
                            break;
                        default:
                            break;
                    }
                }
                this.SetData(url);
                return url;
            }
            return "";
        }

        public object GetAdv(enumAdv k, bool online)
        {
            if (this.Adv != null)
            {
                object obj = null;
                Dictionary<string, string> dic = online == true ? this.LoadAdv()["adv"] : this.Adv["adv"];

                switch (k)
                {
                    case enumAdv.interlaced_only_bottom:
                    case enumAdv.field_to_frame:
                    case enumAdv.ts_muxer:
                    case enumAdv.ts_once:
                    case enumAdv.httpts_password_enable:
                    case enumAdv.g4_gw_as_dns:
                    case enumAdv.ntp_server:
                    case enumAdv.ntp_enable:
                    case enumAdv.time_zone:
                    case enumAdv.hls_buffer_number:
                    case enumAdv.hls_splitter_time:
                    case enumAdv.ts_transport_stream_id:
                    case enumAdv.ts_pmt_start_pid:
                    case enumAdv.ts_start_pid:
                    case enumAdv.ts_tables_version:
                    case enumAdv.ts_rc_mode:
                    case enumAdv.ts_service_name:
                    case enumAdv.ts_service_provider:
                    case enumAdv.vmix_compatible:
                    case enumAdv.audio_only:
                    case enumAdv.video_only:
                    case enumAdv.auto_super_frame_reencode:
                    case enumAdv.slice_split_enable:
                    case enumAdv.slice_split_size:
                    case enumAdv.min_qp:
                    case enumAdv.max_qp:
                    case enumAdv.i_qp:
                    case enumAdv.p_qp:
                    case enumAdv.schedule_restart_enable:
                    case enumAdv.schedule_restart_time:
                    case enumAdv.net_packet_drop_threshold:
                    case enumAdv.remserial_baudrate:
                    case enumAdv.remserial_tcp_port:
                    case enumAdv.csc_enable:
                    case enumAdv.csc_contrast:
                        obj = dic[k.ToString()];
                        break;
                    default:
                        break;
                }
                return obj;
            }
            return null;
        }
        public object GetAdv(enumAdv k)
        {
            return this.GetAdv(k, false);
        }
        public string SetAdv(params enumAdv[] advparams)
        {
            if (this.Adv != null)
            {
                Dictionary<string, string> dic = this.Adv["adv"];
                string url = "/set_adv?";

                foreach (var sp in advparams)
                {
                    switch (sp)
                    {
                        case enumAdv.ntp_server:
                        case enumAdv.ts_service_name:
                        case enumAdv.ts_service_provider:
                            url += "&" + sp.ToString() + "=" + WebUtility.UrlEncode(dic[sp.ToString()]);
                            break;
                        case enumAdv.interlaced_only_bottom:
                        case enumAdv.field_to_frame:
                        case enumAdv.ts_muxer:
                        case enumAdv.ts_once:
                        case enumAdv.httpts_password_enable:
                        case enumAdv.g4_gw_as_dns:
                        case enumAdv.ntp_enable:
                        case enumAdv.time_zone:
                        case enumAdv.hls_buffer_number:
                        case enumAdv.hls_splitter_time:
                        case enumAdv.ts_transport_stream_id:
                        case enumAdv.ts_pmt_start_pid:
                        case enumAdv.ts_start_pid:
                        case enumAdv.ts_tables_version:
                        case enumAdv.ts_rc_mode:
                        case enumAdv.vmix_compatible:
                        case enumAdv.audio_only:
                        case enumAdv.video_only:
                        case enumAdv.auto_super_frame_reencode:
                        case enumAdv.slice_split_enable:
                        case enumAdv.slice_split_size:
                        case enumAdv.min_qp:
                        case enumAdv.max_qp:
                        case enumAdv.i_qp:
                        case enumAdv.p_qp:
                        case enumAdv.schedule_restart_enable:
                        case enumAdv.schedule_restart_time:
                        case enumAdv.net_packet_drop_threshold:
                        case enumAdv.remserial_baudrate:
                        case enumAdv.remserial_tcp_port:
                        case enumAdv.csc_enable:
                        case enumAdv.csc_contrast:
                            url += "&" + sp.ToString() + "=" + dic[sp.ToString()];
                            break;
                        default:
                            break;
                    }
                }
                url = url.Remove(url.IndexOf('&', 0), 1);
                this.SetData(url);
                return url;
            }
            return "";
        }
        public bool ScheduleRestartEnable
        {
            get
            {
                return this.LoadAdv()["adv"][enumAdv.schedule_restart_enable.ToString()] == "1";
            }
            set
            {
                this.SetValue(enumAdv.schedule_restart_enable, value == true ? "1" : "0");
                this.SetAdv(enumAdv.schedule_restart_enable);
            }
        }
        public bool SetScheduleRestart( string t, bool e)
        {
            try
            {
                this.SetValue(enumAdv.schedule_restart_time, t);
                this.SetValue(enumAdv.schedule_restart_enable, e == true ? "1" : "0");
                this.SetAdv(enumAdv.schedule_restart_time, enumAdv.schedule_restart_enable);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public object GetVenc(int v, enumVenc k , bool online)
        {
            if (this.Status != null)
            {
                object obj = null;
                Dictionary<string, string> dic = online == true ? this.LoadStatus()["venc" + v] : this.Status["venc" + v];

                switch (k)
                {
                    case enumVenc.left_pics:
                    case enumVenc.left_stream_bytes:
                    case enumVenc.left_stream_frame:
                    case enumVenc.packs:
                    case enumVenc.enable:
                    case enumVenc.codec:
                    case enumVenc.width:
                    case enumVenc.height:
                    case enumVenc.framerate:
                    case enumVenc.bitrate:
                    case enumVenc.jpg_url0:
                    case enumVenc.mjpg_url:
                    case enumVenc.rtmp_publish_url:
                    case enumVenc.rtmp_status:
                        string op="";
                        obj = dic.TryGetValue(k.ToString(),out op);
                        if (op == null) obj = null;
                        break;
                    default:
                        break;
                }
                return obj;
            }
            return null;
        }
        public object GetVenc(int v, enumVenc k)
        {
            return this.GetVenc(v, k, false);
        }
        public string RtmpPublishStatus(int r)
        {
            try
            {
                string d;
                this.LoadStatus()["venc" + r].TryGetValue(enumVenc.rtmp_status.ToString(), out d);
                return d == null ? "DISABLED" : d == "0" ? "CONNECTED" : "DISCONNECTED";
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }

        public void Reboot()
        {
            this.SetData("/reboot");
        }
        public void Reset()
        {
            this.SetData("/reset");
        }
        public string[] GetValidRes(int res)
        {
            return (new string[] { "0x0", "1920x1080", "1680x1056", "1280x720", "1024x768", "1024x576", "850x480", "720x576", "720x540", "720x480", "720x404", "704x576", "640x480", "640x360", "608x448", "544x480", "480x480", "480x384", "480x360", "480x320", "480x272", "480x270", "400x320", "400x224", "352}480", "352x228", "320x256", "320x240", "320x180", "240x180" })[res].Split('x');
        }
    }
}
 