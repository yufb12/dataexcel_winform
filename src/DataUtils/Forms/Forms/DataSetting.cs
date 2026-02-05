//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Feng  
//{ 
//    public class CitySetting
//    {

//        public static Dictionary<string, List<string>> GetCitys()
//        {
//            Dictionary<string, List<string>> dics = new Dictionary<string, List<string>>();
//            List<string> list=new List<string> ();
//            dics.Add("北京市", list);
//            list.Add("北京市");
//            list = new List<string>();
//            dics.Add("天津市", list);
//            list.Add("天津市");

//            list = new List<string>();
//            dics.Add("河北省", list);
//            list.Add("天津市");   
//            list.Add("石家庄市");   
//            list.Add("唐山市");   
//            list.Add("秦皇岛市");   
//            list.Add("邯郸市");   
//            list.Add("邢台市");   
//            list.Add("保定市");   
//            list.Add("张家口市");   
//            list.Add("承德市");   
//            list.Add("沧州市");   
//            list.Add("廊坊市");
//            list.Add("衡水市");


//            list = new List<string>();
//            dics.Add("山西省", list);
//            list.Add("太原市"); 	
//            list.Add("大同市"); 
//            list.Add("阳泉市"); 
//            list.Add("长治市"); 
//            list.Add("晋城市"); 
//            list.Add("朔州市"); 
//            list.Add("晋中市"); 
//            list.Add("运城市"); 
//            list.Add("忻州市"); 
//            list.Add("临汾市");
//            list.Add("吕梁市");

//            list = new List<string>();
//            dics.Add("内蒙古", list);
//            list.Add("呼和浩特市"); 	 
//            list.Add("包头市"); 	
//            list.Add("赤峰市"); 	
//            list.Add("通辽市"); 	
//            list.Add("鄂尔多斯市"); 	
//            list.Add("呼伦贝尔市"); 	
//            list.Add("巴彦淖尔市"); 	
//            list.Add("乌兰察布市"); 	
//            list.Add("兴安盟"); 	
//            list.Add("锡林郭勒盟");
//            list.Add("阿拉善盟");

//            list = new List<string>();
//            dics.Add("辽宁省", list);
//            list.Add("沈阳市"); 
                   
//            list.Add("大连市"); 
//            list.Add("鞍山市"); 
//            list.Add("抚顺市"); 
//            list.Add("本溪市"); 
//            list.Add("丹东市"); 
//            list.Add("锦州市"); 
//            list.Add("营口市"); 
//            list.Add("阜新市"); 
//            list.Add("辽阳市"); 
//            list.Add("盘锦市"); 
//            list.Add("铁岭市"); 
//            list.Add("朝阳市");
//            list.Add("葫芦岛市");

//            list = new List<string>();
//            dics.Add("吉林省", list);
//            list.Add("长春市");  
//            list.Add("吉林市");
//            list.Add("四平市");
//            list.Add("辽源市");
//            list.Add("通化市");
//            list.Add("白山市");
//            list.Add("松原市");
//            list.Add("白城市");
//            list.Add("延边朝鲜族自治州");

//            list = new List<string>();
//            dics.Add("黑龙江省", list);
//            list.Add("哈尔滨市");   
//            list.Add("齐齐哈尔市");  
//            list.Add("鸡西市");  
//            list.Add("鹤岗市");  
//            list.Add("双鸭山市");  
//            list.Add("大庆市");  
//            list.Add("伊春市");  
//            list.Add("佳木斯市");  
//            list.Add("七台河市");  
//            list.Add("牡丹江市");  
//            list.Add("黑河市");  
//            list.Add("绥化市");
//            list.Add("大兴安岭地区");

//            list = new List<string>();
//            dics.Add("上海市", list);
//            list.Add("上海市");

//            list = new List<string>();
//            dics.Add("江苏省", list);
//            list.Add("南京市");  
          
//            list.Add("无锡市");
//            list.Add("徐州市");
//            list.Add("常州市");
//            list.Add("苏州市");
//            list.Add("南通市");
//            list.Add("连云港市");
//            list.Add("淮安市");
//            list.Add("盐城市");
//            list.Add("扬州市");
//            list.Add("镇江市");
//            list.Add("泰州市");
//            list.Add("宿迁市");


//            list = new List<string>();
//            dics.Add("浙江省", list);
//            list.Add("杭州市");   

//            list.Add("宁波市");
//            list.Add("温州市");
//            list.Add("嘉兴市");
//            list.Add("湖州市");
//            list.Add("绍兴市");
//            list.Add("金华市");
//            list.Add("衢州市");
//            list.Add("舟山市");
//            list.Add("台州市");
//            list.Add("丽水市");


//            list = new List<string>();
//            dics.Add("安徽省", list);
//            list.Add("合肥市");   
                       
//            list.Add("芜湖市");  
//            list.Add("蚌埠市");  
//            list.Add("淮南市");  
//            list.Add("马鞍山市");  
//            list.Add("淮北市");  
//            list.Add("铜陵市");  
//            list.Add("安庆市");  
//            list.Add("黄山市");  
//            list.Add("滁州市");  
//            list.Add("阜阳市");  
//            list.Add("宿州市");  
//            list.Add("巢湖市");  
//            list.Add("六安市");  
//            list.Add("亳州市");  
//            list.Add("池州市");
//            list.Add("宣城市");


//            list = new List<string>();
//            dics.Add("福建省", list);
//            list.Add("福州市"); 
 	
//            list.Add("厦门市"); 
//            list.Add("莆田市"); 
//            list.Add("三明市"); 
//            list.Add("泉州市"); 
//            list.Add("漳州市"); 
//            list.Add("南平市"); 
//            list.Add("龙岩市");
//            list.Add("宁德市");

//            list = new List<string>();
//            dics.Add("江西省", list);
//            list.Add("南昌市"); 
                    
//            list.Add("景德镇市"); 
//            list.Add("萍乡市"); 
//            list.Add("九江市"); 
//            list.Add("新余市"); 
//            list.Add("鹰潭市"); 
//            list.Add("赣州市"); 
//            list.Add("吉安市"); 
//            list.Add("宜春市"); 
//            list.Add("抚州市");
//            list.Add("上饶市");

//            list = new List<string>();
//            dics.Add("山东省", list);
//            list.Add("济南市"); 
                      
//            list.Add("青岛市");
//            list.Add("淄博市");
//            list.Add("枣庄市");
//            list.Add("东营市");
//            list.Add("烟台市");
//            list.Add("潍坊市");
//            list.Add("济宁市");
//            list.Add("泰安市");
//            list.Add("威海市");
//            list.Add("日照市");
//            list.Add("莱芜市");
//            list.Add("临沂市");
//            list.Add("德州市");
//            list.Add("聊城市");
//            list.Add("滨州市");
//            list.Add("荷泽市");


//            list = new List<string>();
//            dics.Add("河南省", list);
//            list.Add("郑州市"); 
                    
//            list.Add("开封市"); 
//            list.Add("洛阳市"); 
//            list.Add("平顶山市"); 
//            list.Add("安阳市"); 
//            list.Add("鹤壁市"); 
//            list.Add("新乡市"); 
//            list.Add("焦作市"); 
//            list.Add("濮阳市"); 
//            list.Add("许昌市"); 
//            list.Add("漯河市"); 
//            list.Add("三门峡市"); 
//            list.Add("南阳市"); 
//            list.Add("商丘市"); 
//            list.Add("信阳市"); 
//            list.Add("周口市");
//            list.Add("驻马店市");


//            list = new List<string>();
//            dics.Add("湖北省", list);
//            list.Add("武汉市"); 
                   
//            list.Add("黄石市"); 
//            list.Add("十堰市"); 
//            list.Add("宜昌市"); 
//            list.Add("襄樊市"); 
//            list.Add("鄂州市"); 
//            list.Add("荆门市"); 
//            list.Add("孝感市"); 
//            list.Add("荆州市"); 
//            list.Add("黄冈市"); 
//            list.Add("咸宁市"); 
//            list.Add("随州市"); 
//            list.Add("恩施土家族苗族自治州");
//            list.Add("神农架");

//            list = new List<string>();
//            dics.Add("湖南省", list);
//            list.Add("长沙市"); 
                    
//            list.Add("株洲市"); 
//            list.Add("湘潭市"); 
//            list.Add("衡阳市"); 
//            list.Add("邵阳市"); 
//            list.Add("岳阳市"); 
//            list.Add("常德市"); 
//            list.Add("张家界市"); 
//            list.Add("益阳市"); 
//            list.Add("郴州市"); 
//            list.Add("永州市"); 
//            list.Add("怀化市"); 
//            list.Add("娄底市");
//            list.Add("湘西土家族苗族自治州");


//            list = new List<string>();
//            dics.Add("广东省", list);
//            list.Add("广州市"); 
                 
//            list.Add("韶关市");
//            list.Add("深圳市");
//            list.Add("珠海市");
//            list.Add("汕头市");
//            list.Add("佛山市");
//            list.Add("江门市");
//            list.Add("湛江市");
//            list.Add("茂名市");
//            list.Add("肇庆市");
//            list.Add("惠州市");
//            list.Add("梅州市");
//            list.Add("汕尾市");
//            list.Add("河源市");
//            list.Add("阳江市");
//            list.Add("清远市");
//            list.Add("东莞市");
//            list.Add("中山市");
//            list.Add("潮州市");
//            list.Add("揭阳市");
//            list.Add("云浮市");


//            list = new List<string>();
//            dics.Add("广西", list);
//            list.Add("南宁市"); 
               
//            list.Add("柳州市");
//            list.Add("桂林市");
//            list.Add("梧州市");
//            list.Add("北海市");
//            list.Add("防城港市");
//            list.Add("钦州市");
//            list.Add("贵港市");
//            list.Add("玉林市");
//            list.Add("百色市");
//            list.Add("贺州市");
//            list.Add("河池市");
//            list.Add("来宾市");
//            list.Add("崇左市");

//            list = new List<string>();
//            dics.Add("海南省", list);
//            list.Add("海口市");
//            list.Add("三亚市");

//            list = new List<string>();
//            dics.Add("重庆市", list);
//            list.Add("重庆市");


//            list = new List<string>();
//            dics.Add("四川省", list);
//            list.Add("成都市");
               
//            list.Add("自贡市");
//            list.Add("攀枝花市");
//            list.Add("泸州市");
//            list.Add("德阳市");
//            list.Add("绵阳市");
//            list.Add("广元市");
//            list.Add("遂宁市");
//            list.Add("内江市");
//            list.Add("乐山市");
//            list.Add("南充市");
//            list.Add("眉山市");
//            list.Add("宜宾市");
//            list.Add("广安市");
//            list.Add("达州市");
//            list.Add("雅安市");
//            list.Add("巴中市");
//            list.Add("资阳市");
//            list.Add("阿坝藏族羌族自治州");
//            list.Add("甘孜藏族自治州");
//            list.Add("凉山彝族自治州");


//            list = new List<string>();
//            dics.Add("贵州省", list);
//            list.Add("贵阳市");
                    
//            list.Add("六盘水市");
//            list.Add("遵义市");
//            list.Add("安顺市");
//            list.Add("铜仁地区");
//            list.Add("黔西南布依族苗族自治州");
//            list.Add("毕节地区");
//            list.Add("黔东南苗族侗族自治州");
//            list.Add("黔南布依族苗族自治州");


//            list = new List<string>();
//            dics.Add("云南省", list);
//            list.Add("昆明市");
                    
//            list.Add("曲靖市");
//            list.Add("玉溪市");
//            list.Add("保山市");
//            list.Add("昭通市");
//            list.Add("丽江市");
//            list.Add("思茅市");
//            list.Add("临沧市");
//            list.Add("楚雄彝族自治州");
//            list.Add("红河哈尼族彝族自治州");
//            list.Add("文山壮族苗族自治州");
//            list.Add("西双版纳傣族自治州");
//            list.Add("大理白族自治州");
//            list.Add("德宏傣族景颇族自治州");
//            list.Add("怒江傈僳族自治州");
//            list.Add("迪庆藏族自治州");

//            list = new List<string>();
//            dics.Add("西藏", list);
//            list.Add("拉萨市");
                    
//            list.Add("昌都地区");
//            list.Add("山南地区");
//            list.Add("日喀则地区");
//            list.Add("那曲地区");
//            list.Add("阿里地区");
//            list.Add("林芝地区");

//            list = new List<string>();
//            dics.Add("陕西省", list);
//            list.Add("西安市");
                      
//            list.Add("铜川市");
//            list.Add("宝鸡市");
//            list.Add("咸阳市");
//            list.Add("渭南市");
//            list.Add("延安市");
//            list.Add("汉中市");
//            list.Add("榆林市");
//            list.Add("安康市");
//            list.Add("商洛市");


//            list = new List<string>();
//            dics.Add("甘肃省", list);
//            list.Add("兰州市");
                    
//            list.Add("嘉峪关市");
//            list.Add("金昌市");
//            list.Add("白银市");
//            list.Add("天水市");
//            list.Add("武威市");
//            list.Add("张掖市");
//            list.Add("平凉市");
//            list.Add("酒泉市");
//            list.Add("庆阳市");
//            list.Add("定西市");
//            list.Add("陇南市");
//            list.Add("临夏回族自治州");
//            list.Add("甘南藏族自治州");


//            list = new List<string>();
//            dics.Add("青海省", list);
//            list.Add("西宁市");
                      
//            list.Add("海东地区");
//            list.Add("海北藏族自治州");
//            list.Add("黄南藏族自治州");
//            list.Add("海南藏族自治州");
//            list.Add("果洛藏族自治州");
//            list.Add("玉树藏族自治州");
//            list.Add("海西蒙古族藏族自治州");

//            list = new List<string>();
//            dics.Add("宁夏", list);
//            list.Add("银川市");
                     
//            list.Add("石嘴山市");
//            list.Add("吴忠市");
//            list.Add("固原市");
//            list.Add("中卫市");

//            list = new List<string>();
//            dics.Add("新疆", list);
//            list.Add("乌鲁木齐市");
                 
//            list.Add("克拉玛依市");
//            list.Add("吐鲁番地区");
//            list.Add("哈密地区");
//            list.Add("昌吉回族自治州");
//            list.Add("博尔塔拉蒙古自治州");
//            list.Add("巴音郭楞蒙古自治州");
//            list.Add("阿克苏地区");
//            list.Add("克孜勒苏柯尔克孜自治州");
//            list.Add("喀什地区");
//            list.Add("和田地区");
//            list.Add("伊犁哈萨克自治州");
//            list.Add("塔城地区");
//            list.Add("阿勒泰地区");
//            list.Add("石河子市");
//            list.Add("阿拉尔市");
//            list.Add("图木舒克市");
//            list.Add("五家渠市");

//            list = new List<string>();
//            dics.Add("香港", list);
//            list.Add("香港");

//            list = new List<string>();
//            dics.Add("澳门", list);
//            list.Add("澳门");

//            list = new List<string>();
//            dics.Add("台湾省", list);
//            list.Add("台湾省");
 
//            return dics;
//        }
//    }
//}
