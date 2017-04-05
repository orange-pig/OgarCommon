using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace OgarCommon
{
    public class ProtocolTypeHelper
    {
        private static Dictionary<ProtocolType, string> TypeAndDesc;

        private static Dictionary<ProtocolType, string> TypeAndName;
        static ProtocolTypeHelper()
        {
            TypeAndDesc = new Dictionary<ProtocolType, string>(){
                {ProtocolType.Unknown,"所有协议"},
                {ProtocolType.Icmp,"网际消息控制协议"},
                {ProtocolType.Igmp,"网际组管理协议"},
                {ProtocolType.Ggp,"网关到网关协议"},
                {ProtocolType.IPv4,"Internet Protocol version 4"},
                {ProtocolType.Tcp,"传输控制协议"},
                {ProtocolType.Pup,"PARC 通用数据包协议"},
                {ProtocolType.Udp,"用户数据报协议"},
                {ProtocolType.Idp,"Internet 数据报协议"},
                {ProtocolType.IPv6,"Internet 协议版本 6 (IPv6)"},
                {ProtocolType.IPv6RoutingHeader,"IPv6 路由头"},
                {ProtocolType.IPv6FragmentHeader,"IPv6 片段头"},
                {ProtocolType.IPSecEncapsulatingSecurityPayload,"IPv6 封装式安全措施负载头"},
                {ProtocolType.IPSecAuthenticationHeader,"IPv6 身份验证头"},
                {ProtocolType.IcmpV6,"用于 IPv6 的 Internet 控制消息协议"},
                {ProtocolType.IPv6NoNextHeader,"IPv6 No Next 头"},
                {ProtocolType.IPv6DestinationOptions,"IPv6 目标选项头"},
                {ProtocolType.ND,"网络磁盘协议（非正式）"},
                {ProtocolType.Raw,"原始 IP 数据包协议"},
                {ProtocolType.Ipx,"Internet 数据包交换协议"},
                {ProtocolType.Spx,"顺序包交换协议"},
                {ProtocolType.SpxII,"顺序包交换协议第 2 版"}
            };
            TypeAndName = new Dictionary<ProtocolType, string>(){
                {ProtocolType.Unknown,"All"},
                {ProtocolType.Icmp,"Icmp"},
                {ProtocolType.Igmp,"Igmp"},
                {ProtocolType.Ggp,"Ggp"},
                {ProtocolType.IPv4,"IPv4"},
                {ProtocolType.Tcp,"Tcp"},
                {ProtocolType.Pup,"Pup"},
                {ProtocolType.Udp,"Udp"},
                {ProtocolType.Idp,"Idp"},
                {ProtocolType.IPv6,"IPv6"},
                {ProtocolType.IPv6RoutingHeader,"IPv6RoutingHeader"},
                {ProtocolType.IPv6FragmentHeader,"IPv6FragmentHeader"},
                {ProtocolType.IPSecEncapsulatingSecurityPayload,"IPSecEncapsulatingSecurityPayload"},
                {ProtocolType.IPSecAuthenticationHeader,"IPSecAuthenticationHeader"},
                {ProtocolType.IcmpV6,"IcmpV6"},
                {ProtocolType.IPv6NoNextHeader,"IPv6NoNextHeader"},
                {ProtocolType.IPv6DestinationOptions,"IPv6DestinationOptions"},
                {ProtocolType.ND,"ND"},
                {ProtocolType.Raw,"Raw"},
                {ProtocolType.Ipx,"Ipx"},
                {ProtocolType.Spx,"Spx"},
                {ProtocolType.SpxII,"SpxII"}
            };
        }

        public static ProtocolType GetProtocolTypeByDesc(string desc)
        {
            ProtocolType ret = ProtocolType.Unknown;

            foreach (var t in TypeAndDesc)
            {
                if (t.Value == desc)
                {
                    ret = t.Key;
                    break;
                }
            }

            return ret;
        }

        public static ProtocolType GetProtocolTypeByName(string name)
        {
            ProtocolType ret = ProtocolType.Unknown;

            foreach (var t in TypeAndName)
            {
                if (t.Value == name)
                {
                    ret = t.Key;
                    break;
                }
            }

            return ret;
        }

        public static string GetDesc(ProtocolType type)
        {
            return TypeAndDesc[type];
        }

        public static string GetName(ProtocolType type)
        {
            string ret;
            try
            {
                ret = TypeAndName[type];
            }
            catch (KeyNotFoundException ex)
            {
                ret = "Unkown";
            }

            return ret;
        }

        public static List<string> GetDescList()
        {
            return TypeAndDesc.Values.ToList();
        }

        public static List<string> GetNameList()
        {
            return TypeAndName.Values.ToList();
        }
    }
}
