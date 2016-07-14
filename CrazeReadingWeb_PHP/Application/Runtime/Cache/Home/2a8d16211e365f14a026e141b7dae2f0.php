<?php if (!defined('THINK_PATH')) exit();?><div id="cnblogs_post_body"><p><strong>1）  <span style="font-family: 宋体;">背景</span></strong></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">建设云平台的基础框架，用于支持各类云服务的业务的构建及发展。</span></span></p>
<p><strong>2）  <span style="font-family: 宋体;">基础服务</span></strong></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">根据目前对业务的理解和发展方向，总结抽象出以下几个基础服务，如图所示</span></span></p>
<p style="text-align: left;"><img src="http://static.oschina.net/uploads/space/2016/0712/180411_tsDA_2379842.png" alt="" width="783" height="239" data-cke-saved-src="http://static.oschina.net/uploads/space/2016/0712/180411_tsDA_2379842.png"></p>
<p><span style="font-size: 6.5pt;"> </span></p>
<p><strong>3）  <span style="font-family: 宋体;">概要说明</span></strong></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">基础服务的发展会根据业务的发展，调整和完善，也会不断的改进，演变及完善；当然根据目前公司的现状和对基础服务的迫切程度，基础服务各模块的定位和发展预期将如下所述。</span></span></p>
<p><span style="font-size: 16px;"><strong>1）     </strong><strong><span style="font-family: 宋体;">数据库中间件</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">对多种类型数据库的支持需求迫切，如同时支持mysql<span style="font-family: 宋体;">，orcale<span style="font-family: 宋体;">，sqlserver<span style="font-family: 宋体;">这些数据库。最多改动少量代码就可以在多种数据库类型中切换。</span></span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">尽量不要让开发人员写sql<span style="font-family: 宋体;">代码，因为原有的开发人员开发方式是采用linq<span style="font-family: 宋体;">的方式，大部分开发的业务是winform<span style="font-family: 宋体;">类型的业务。</span></span></span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">目前采用entity framework<span style="font-family: 宋体;">，因entity framework <span style="font-family: 宋体;">本身采用linq<span style="font-family: 宋体;">方式编程，自身能够解析linq<span style="font-family: 宋体;">为sql<span style="font-family: 宋体;">，且兼容多种数据库类型的查询。Entity framework <span style="font-family: 宋体;">在.net <span style="font-family: 宋体;">中的流行程度较高，代码开源，版本发展较快，且拥有大量应用文档和学习资料，相比较同类型的框架而言，当首选之。</span></span></span></span></span></span></span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;">Entity framework <span style="font-family: 宋体;">的采用只是临时的方案，因为业务的需要会持续比较久的时间。</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">从高性能的服务来看，linq to sql<span style="font-family: 宋体;">的过程必然会有性能损失，即便框架做了解析的缓存，但是也无法避免这些问题。一些复杂语句的查询，linq to sql <span style="font-family: 宋体;">目前也会出现意外的解析结果，复杂的语句查询难以用linq<span style="font-family: 宋体;">表达。如果不是对linq to sql <span style="font-family: 宋体;">这种方式较熟练和关注性能的人，一般写法上也会导致性能问题。</span></span></span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">从数据库的角度看，目前业务暂时还使用同一个数据库，未来业务会采用多个数据库，多张数据表。这一点entity framework <span style="font-family: 宋体;">后面会涉及到分库的支持和分表的支持，显然即便修改源码也很头疼。而且多个数据源，多个数据库类型的支持，意味着同一个业务要涉及到多种数据库下面性能的调优和运维，特别是涉及到版本升级的数据迁移，要兼容多种数据库，意味着工作量真心不小。</span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来方向：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用单一类型的数据库，会有一个支持sql<span style="font-family: 宋体;">编写直连数据库，支持分库分表的分布式数据库，自动管理数据库连接池，自动提供性能分析及预警等的数据库中间件。</span></span></span></p>
<p><span style="font-size: 6.5pt;"> </span></p>
<p><span style="font-size: 16px;"><strong>2）     </strong><strong>TCP</strong><strong><span style="font-family: 宋体;">服务框架</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">用于采集程序，采集设备和服务器的直连，发送采集信息。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">服务器端反向通知连接程序或设备，即时通知信息。</span></span></p>
<p><span style="font-size: 18px;">3）     <span style="font-family: 宋体;">与工作站的通信环境（云平台采用ActiveMQ<span style="font-family: 宋体;">）,<span style="font-family: 宋体;">连接第三方设备（采用signalr asp.net<span style="font-family: 宋体;">）。</span></span></span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">暂时保持与工作站的通信环境（云平台采用ActiveMQ<span style="font-family: 宋体;">）,<span style="font-family: 宋体;">连接第三方设备（采用signalr<span style="font-family: 宋体;">集成入asp.net<span style="font-family: 宋体;">）这种方案。因为公司目前采用C#<span style="font-family: 宋体;">编程，这两块技术选型都有相应的C#<span style="font-family: 宋体;">客户端或者C#<span style="font-family: 宋体;">的解决方案的一些示例；故使用起来问题应该不大，但是遇到的问题也不会少。若遇到性能问题，可以简单的通过扩展多个ActiveMQ<span style="font-family: 宋体;">和 <span style="font-family: 宋体;">应用服务的负载均衡来解决。</span></span></span></span></span></span></span></span></span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">其他方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用redis<span style="font-family: 宋体;">或者rabbitmq<span style="font-family: 宋体;">之类的类似解决方案，个人倾向与redis <span style="font-family: 宋体;">的发布订阅机制解决，性能不算差（听闻过上规模的使用案例及跨语言客户端sdk<span style="font-family: 宋体;">）（且可以统一缓存的使用框架，便于维护。）</span></span></span></span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">无论采用redis<span style="font-family: 宋体;">，activemq<span style="font-family: 宋体;">，rabbitmq<span style="font-family: 宋体;">之类的哪种消息队列方式解决，都无法避免本质的性能问题，因为这些框架本身是用来解决消息队列的，因为其内存消息转发机制，故而用于一些即时通讯，常用于解决内网环境的一些应用交互。目前的场景会应用到广域网环境与工作站进行通信，业内类似的解决方案也曾有人使用过，但是也会经常出现activemq <span style="font-family: 宋体;">内存满或者莫名死掉的情况。</span></span></span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">采用signalr <span style="font-family: 宋体;">应用挂载到asp.net <span style="font-family: 宋体;">上面的使用方式，经过一些第三者开发的经验，也会出现稍微高并发就出现性能问题或者死掉的情况。Signalr <span style="font-family: 宋体;">常用于.net <span style="font-family: 宋体;">技术，也有简单的使用案列，但是还没有成熟的上规模的使用案例和场景。</span></span></span></span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来方向：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用java<span style="font-family: 宋体;">的NIO<span style="font-family: 宋体;">思想或者Windows <span style="font-family: 宋体;">完成端口思想,<span style="font-family: 宋体;">搭建纯粹的TCP socket<span style="font-family: 宋体;">服务是解决本质的一个方案，一般一台服务器能够承载10<span style="font-family: 宋体;">万的连接，几千的活动连接（具体看服务器配置等情况）不会有问题（而旧方案可能承载几千，上百的活动连接就会出现性能问题）。</span></span></span></span></span></span></span></span></p>
<p><span style="font-size: 6.5pt;"> </span></p>
<p><span style="font-size: 16px;"><strong>3）     </strong><strong><span style="font-family: 宋体;">认证中心</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">原有工作站内网子系统的登陆验证，外网设备登录验证，云平台用户登录验证。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">云平台用户菜单权限获取，操作权限获取。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">自行研发公司特有业务的认证中心平台，目前仅第一个版本。包含</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">设备管理，子系统管理，云平台用户管理和权限管理等</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">第三方使用的登录接口，用户菜单权限接口，用户操作权限接口。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">以上功能目前能够满足现有公司的业务。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">目前比较简单，通过token<span style="font-family: 宋体;">授权，无名加密，无appid<span style="font-family: 宋体;">和serect<span style="font-family: 宋体;">秘钥授权之类的。故而没有较强的安全机制，但是能够满足实际开发。而且目前的公司业务对于安全的要求并不高。</span></span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">通信性能不高，因为目前采用Http<span style="font-family: 宋体;">协议进行通信，本身通信性能不高。而且认证中心将承载所有业务的认证，基本上所有云项目模块等业务都会将请求汇聚到认证中心的接口上，在后续公司流量的发展上必然会出现第一个出现接口上的性能问题。</span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来方向：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">平台所有的接口实现内部必须有redis<span style="font-family: 宋体;">缓存，平台接口客户端使用要有sdk<span style="font-family: 宋体;">封装（在sdk<span style="font-family: 宋体;">内部做客户端缓存，哪怕默认5 s<span style="font-family: 宋体;">的缓存）</span></span></span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">平台的所有接口后续接到“<span style="font-family: 宋体;">高性能服务中心”<span style="font-family: 宋体;">，走TCP<span style="font-family: 宋体;">连接池的通信方式实现，提高内部通信的性能。</span></span></span></span></span></p>
<p><span style="font-size: 16px;"><strong>4）     </strong><strong><span style="font-family: 宋体;">服务中心（个人开源地址：<a href="http://git.oschina.net/chejiangyi/Dyd.BaseService.ServiceCenter" rel="nofollow" target="_self">http://git.oschina.net/chejiangyi/Dyd.BaseService.ServiceCenter</a>）</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">项目之间出现互相调用的业务耦合，目前采用dll<span style="font-family: 宋体;">的方式调用，但是出现dll<span style="font-family: 宋体;">更新出错及管理等情况，导致开发人员认为效率不高。</span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">公司迫切希望采用微服务/soa<span style="font-family: 宋体;">的架构方式来剥离项目的业务耦合，简化上下游业务调用的管理方式。</span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">暂时采用Http restful<span style="font-family: 宋体;">类似的方式提供服务化的接口，供第三方接口调用，同时这也符合soa<span style="font-family: 宋体;">服务化的架构思想。</span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">通过api view <span style="font-family: 宋体;">自动公开接口文档，上下游之间调用调试，方便开发人员沟通协调。</span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">个人经验：服务化的接口方式有效的,<span style="font-family: 宋体;">对业务沟通也是非常有帮助的，但是未必能够真正的在效率上得到本质的提升。但是对于项目的模块化管理应该有较好的帮助。</span></span></span></p>
<p><span style="font-size: 18px;">2）     Http<span style="font-family: 宋体;">的接口通信方式效率并不高，作为服务框架必然是走TCP<span style="font-family: 宋体;">的内部通信，性能才会有明显提升。</span></span></span></p>
<p><span style="font-size: 18px;">3）     <span style="font-family: 宋体;">服务治理，协调方面的问题为考虑，如没有考虑调用链死循环，调用链上的性能导致雪崩，上下游服务监控，上下游客户端服务变更历史记录及通知，上下游客户端服务协议耦合剥离，服务化层面的负载均衡和故障转移等等众多问题。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来方向：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">自研服务中心，将性能，服务治理，协调等工作从业务开发中抽离抽象出来，业务开发只需要关注无状态的业务服务开发即可。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">所有内部的业务全部剥离（不仅仅是耦合的业务），迁移到内部的服务中心，如果内部服务需要对第三方公开，可以提供Http<span style="font-family: 宋体;">的开放网关服务进行调用，网关层会做一些授权管理等工作，网关自身做负载均衡。</span></span></span></p>
<p><span style="font-size: 16px;"><strong>5）     </strong><strong><span style="font-family: 宋体;">统一监控（个人开源地址：<a href="http://git.oschina.net/chejiangyi/Dyd.BaseService.Monitor" rel="nofollow">http://git.oschina.net/chejiangyi/Dyd.BaseService.Monitor</a>）</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">项目处于前期研发阶段，没有较大规模的服务器集群，没有遇到多版本接口兼容，没有遇到线上监控问题和线上排查问题，性能问题的痛苦，对这些情况还不了解和敏感。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">开发人员希望解决项目开发调试时候，错误日志及错误日志的堆栈问题，调用路径问题排查。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">采用Http Restful <span style="font-family: 宋体;">服务化业务接口的方式，应该能缓解项目开发调试的问题。（开发调试问题以前没遇到过，应该跟原来架构和技术采用wcf<span style="font-family: 宋体;">等方式有关导致）</span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">搭建分布式监控平台，因为是本人已有开源的项目，使用起来问题不大。能够解决很多云上服务器管理，性能监控及预警，sql<span style="font-family: 宋体;">性能监控，api<span style="font-family: 宋体;">接口性能监控，统一错误日志等。</span></span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">个人还不是特别确切了解目前项目开发人员调试项目开发过程中，对日志问题真正迫切的本质原因，也没深刻体验（一直开发以来没有遇到问题难调试的问题，可能现有公司项目架构方式关系密切），所以不知道是否能够解决。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">目前分布式监控平台是在原有公司开发的简化版本，为了实现整体项目架构的监控那块的抽象和布局而研发的。性能和功能上还有很多的优化和改进空间。（当然支持公司的现状还是绰绰有余）</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来方向：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">根据公司的业务对监控的需求，还需要不断的改进和完善监控平台。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">监控平台的功能和性能需要完善，底层将使用nosql<span style="font-family: 宋体;">来存储实现。</span></span></span></p>
<p><span style="font-size: 16px;"><strong>6）     </strong><strong><span style="font-family: 宋体;">配置中心（个人开源地址：<a href="http://git.oschina.net/chejiangyi/Dyd.BaseService.ConfigManager" rel="nofollow" target="_blank">http://git.oschina.net/chejiangyi/Dyd.BaseService.ConfigManager</a>）</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">目前公司有类似配置中心的功能，用于基本的业务配置的使用，比较简单。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">云这块业务尚处于简单的业务模型和业务状态，未遇到真正线上复杂的业务和业务剥离的需求，及异步化的功能点，统计类的功能等等，对分布式配置中心的本质需求和问题还没有真正暴露出来。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">依旧使用原有的配置中心功能，同时分布式的配置中心也会搭建。原有的配置中心适合业务配置的存储，现有的配置中心可以用于业务配置的存储，也可以用于分布式架构的环境配置协调问题。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">会维持两套配置中心的运维，在业务架构上比较难以区别，业务上容易混乱。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来方向：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">两块配置中心将根据业务的需求和方向，在一定程度上进行融合。但就目前的公司精力不会着重这块。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">配置中心将根据公司的业务发展，也会继续演变出更多的功能，不过暂时不明朗。</span></span></p>
<p><span style="font-size: 16px;"><strong>7）     </strong><strong><span style="font-family: 宋体;">消息队列（个人开源地址：<a href="http://git.oschina.net/chejiangyi/Dyd.BusinessMQ" rel="nofollow" target="_blank">http://git.oschina.net/chejiangyi/Dyd.BusinessMQ</a>）</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">目前公司在云平台端与工作站异步通信是通过ActiveMQ<span style="font-family: 宋体;">进行的。</span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">云平台项目还处于前期研发起步阶段，业务复杂度还不够，对性能的要求不高，也未涉及同一业务异步化拆分和解耦。</span></span></p>
<p><span style="font-size: 18px;">3）     <span style="font-family: 宋体;">公司的采集方面的业务还未做到真正的大规模分析，大规模采集的场景。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">出于公司架构统一的现状考虑，暂时采用ActiveMQ<span style="font-family: 宋体;">，也方便java<span style="font-family: 宋体;">，C#<span style="font-family: 宋体;">等跨语言的异步通信。当然也仅仅能应用与异步化的简单的即时通信效果。</span></span></span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;">ActiveMQ <span style="font-family: 宋体;">只能作为异步的即时通信暂时使用，就目前的性能和稳定性来说，并不是长远之计。</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">若是为了持久化的Tcp<span style="font-family: 宋体;">通信，未来自己会有TCP<span style="font-family: 宋体;">服务的搭建来确保。</span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">若是为了消息队列的通信，未来更多考虑消息的堆积性能，消息的高稳定性和高可靠性（不能丢失消息）。</span></span></p>
<p><span style="font-size: 18px;">3）     <span style="font-family: 宋体;">若是考虑消息队列收集消息便于后续采集分析，未来更多考虑类似kafka<span style="font-family: 宋体;">的方案。</span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来发展：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">消息队列有众多的解决方案，也有众多的一些特性适用于不同的业务场景。针对这些不同的场景和方案，个人会做如下考虑：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">自建的一套消息队列平台，自建的消息队列可以剥离底层的存储引擎，通过不同的存储引擎的特性，达到适用不同场景和方案的目的。（如存储引擎为redis<span style="font-family: 宋体;">，ssdb<span style="font-family: 宋体;">，数据库等，即便实现逻辑相同，但是性能不同，可靠性表现也不同）</span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">自建的一套消息队列中间件，可以剥离具体的消息队列实现，抽象出常规消息队列的使用方式。仅通过修改连接字符串或者配置类，就能实现不同消息平台的切换。（如底层消息服务可能是activemq<span style="font-family: 宋体;">，rabbitmq<span style="font-family: 宋体;">，redis<span style="font-family: 宋体;">，kafka<span style="font-family: 宋体;">，对上层业务可以是透明，甚至无缝切换）</span></span></span></span></span></span></p>
<p><span style="font-size: 16px;"><strong>8）     </strong><strong><span style="font-family: 宋体;">任务调度平台（个人开源地址：<a href="http://git.oschina.net/chejiangyi/Dyd.BaseService.TaskManager" rel="nofollow" target="_blank">http://git.oschina.net/chejiangyi/Dyd.BaseService.TaskManager</a>）</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">公司目前业务尚处于前期，未有业务需求有类似后台任务统计，后台任务消费之类的业务需求。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">任务调度平台是所有基础服务的一个基础环节，目前也仅在基础服务部署中使用。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">个人开源的分布式任务调度平台。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">分布式任务调度平台目前仅属于一个简单的任务调度平台，未来的发展方向还有很大的空间。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来发展：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">所有公司的业务都被视为一个业务任务，所有的业务任务都将被挂载到任务调度平台，任务调度平台会根据分布式集群的负载情况，自动分配集群服务器用于业务的负载均衡和故障转移等资源的调度和协调。（如所有的接口服务，所有的后台任务，所有的消息消费任务等等）</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">任务调度平台也可称为类似于hadoop<span style="font-family: 宋体;">之类的大数据处理，实时计算平台，用于批量处理实时的，非实时的一些动态的流式的任务创建，回收，协调。（如类似爬虫之类的采集业务，和算法分析任务等）</span></span></span></p>
<p><span style="font-size: 16px;"><strong>9）     </strong><strong><span style="font-family: 宋体;">分布式缓存（个人开源地址：<a href="http://git.oschina.net/chejiangyi/XXF.BaseService.DistributedCache" rel="nofollow" target="_blank">http://git.oschina.net/chejiangyi/XXF.BaseService.DistributedCache</a>）</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">目前公司的业务还不需要用到分布式缓存的使用，除了认证中心这块应该在后续涉及到一些性能问题。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">个人开源的分布式缓存中间件。（目前实现的是基于memcached<span style="font-family: 宋体;">协议的一个统一的分布式缓存框架）</span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">仅支持基础的分布式缓存框架，整体数据结构比较简单（key <span style="font-family: 宋体;">定时过期失效），但是也是缓存中最实用的功能。</span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来发展：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">支持更多的协议，如redis<span style="font-family: 宋体;">的通信协议。及更多底层存储框架的抽象。（每种缓存框架都有特定的使用场景和微妙的差别）</span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">分布式缓存的统一性能监控；一致性哈希的支持便于实现定制的故障转移方案，避免雪崩等缓存失效场景。</span></span></p>
<p><span style="font-size: 18px;">3）     <span style="font-family: 宋体;">根据公司的业务支持其他缓存场景，如本地缓存一致性（协同分布式消息队列实现）的支持。</span></span></p>
<p><span style="font-size: 16px;"><strong>10）  </strong><strong><span style="font-family: 宋体;">文件服务</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">公司目前的采集的业务将信息都存在本地的应用服务器，以文件形式存储。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">公司的采集业务信息文件，需要持久保存。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">暂时保持现状。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">无论从容量的扩容和性能的角度看，单独的文件服务器是一个长远的必然需求。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">目前的业务可能涉及到文件的连续存储和文件部分内容的读取的需求，就市面上的开源文件服务可能不满足需求。</span></span></p>
<p><span style="font-size: 18px;">3）     <span style="font-family: 宋体;">个人现在对公司关于文件服务的业务需求，还不是特别了解。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来发展：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">考虑自研分布式文件服务，读取性能未必胜于市面的开源文件服务。（自研的文件服务应该还是基于windows<span style="font-family: 宋体;">文件管理结构），但是灵活度会更高。</span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">自研的分布式文件服务sdk<span style="font-family: 宋体;">，要在使用上抽象，并兼容公司的底层存储差异（有些大文件存储可能还是使用第三方存储，但是对于开发来说是透明无感知的）。</span></span></span></p>
<p><span style="font-size: 16px;"><strong>11）  </strong><strong><span style="font-family: 宋体;">日志平台</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">公司目前对于项目调试的困难，导致对日志平台的需求。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">公司的业务暂时还不需要基于日志的分析需求，对于大容量日志的记录及基于日志的堆栈调用链记录需求。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">暂时通过监控平台的错误日志和本地的错误日志打印，解决目前对错误调试的需求。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">监控平台也支持常规业务日志的打印，但是此业务日志的打印不支持大容量的需求。（过多打印会造成自身程序阻塞）</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">监控平台也支持常规业务日志的打印，但是此业务日志的打印不支持大容量的需求。（过多打印会造成自身程序阻塞）。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">不支持调用链的日志记录及分析。不支持大容量的日志记录，不支持日志的毫秒级查找，便于问题定位。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来发展：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">日志平台未来会自行研发（或者结合第三方开源），类似于目前开源的elk<span style="font-family: 宋体;">。平台的定位是大容量日志的收集，挖掘，分析，排查。</span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">更多的是结合自身的业务，和对未来业务发展的规划，对于日志平台的基础功能做特定的功能或者统计报表展现。</span></span></p>
<p><span style="font-size: 16px;"><strong>12）  </strong><strong><span style="font-family: 宋体;">开放接口平台（个人开源地址：<a href="http://git.oschina.net/chejiangyi/ApiView" rel="nofollow">http://git.oschina.net/chejiangyi/ApiView</a>）</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">公司的业务急切需要通过soa/<span style="font-family: 宋体;">微服务的方式提供接口，供第三方开发人员使用。</span></span></span></p>
<p><span style="font-size: 18px;">2）     Soa<span style="font-family: 宋体;">业务上下游之间需要维护文档，便于沟通和调试。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">个人开源的appview <span style="font-family: 宋体;">开放接口平台。类似swagger<span style="font-family: 宋体;">。</span></span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">目前开放接口平台实现很简单，功能也非常精简通用。还欠缺一些管理功能，比如版本变更记录和上下游版本变更通知等。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来发展：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">开放接口平台会根据公司实际的问题和需求不断的完善功能，如根据公司的接口约定，自动检测并提醒不规范的接口。自动记录版本变更，自动邮件通知下游调用方接口变更，自动化的接口治理等功能。</span></span></p>
<p><span style="font-size: 6.5pt;"> </span></p>
<p><span style="font-size: 16px;"><strong>13）  </strong><strong><span style="font-family: 宋体;">分布式部署平台</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">公司的云平台业务尚在初期，流量远远没有上来，也没有任何性能问题。</span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">云平台的部署还没有考虑到分布式部署发布和运维的问题，也没有秒级全平台部署，版本管理，版本回滚的需求。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">暂时前提先考虑人工多服务器发布解决。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">人工解决，在真实环境中往往出很多问题，毕竟人是最容易犯错的。所以公司上轨道后，往往采用全自动部署发布的问题。</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来发展：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">自研一套分布式部署发布的平台，做到版本管理，异常回滚，分布式部署等问题。（这个实现并不复杂，够用即可）</span></span></p>
<p><span style="font-size: 16px;"><strong>14）  </strong><strong><span style="font-family: 宋体;">开放</span></strong><strong>Api</strong><strong><span style="font-family: 宋体;">网关</span></strong></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">公司现状：</span></span></p>
<p><span style="font-size: 18px;">1)        <span style="font-family: 宋体;">公司目前采用WCF<span style="font-family: 宋体;">的方式公开服务，调试和使用都很麻烦。</span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">采用方案：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">即将采用http <span style="font-family: 宋体;">直接公开soa<span style="font-family: 宋体;">业务服务的方式解决问题，这种方式粗暴但也非常有效。</span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">后面服务中心开发稳定后，所有业务将会迁移到服务中心，所有业务通过tcp<span style="font-family: 宋体;">连接池访问，提高通信效率，从而提升性能和响应时间。</span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">方案弊端：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">第三方开发人员想通过第三方api<span style="font-family: 宋体;">访问，则往往不支持。</span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">未来发展：</span></span></p>
<p><span style="font-size: 18px;">1）     <span style="font-family: 宋体;">开放api<span style="font-family: 宋体;">网关，将所有内网的服务api<span style="font-family: 宋体;">，对可以通过Http<span style="font-family: 宋体;">的形式进行转发访问，Http<span style="font-family: 宋体;">网关和服务中心保持高性能通信。</span></span></span></span></span></span></p>
<p><span style="font-size: 18px;">2）     <span style="font-family: 宋体;">开放api<span style="font-family: 宋体;">网关遇到性能问题，则负载均衡即可。</span></span></span></p>
<p><span style="font-size: 18px;">3）     <span style="font-family: 宋体;">开放api<span style="font-family: 宋体;">网关将管理对外开放的api<span style="font-family: 宋体;">授权问题，api<span style="font-family: 宋体;">访问频率控制，api<span style="font-family: 宋体;">访问权限控制，api<span style="font-family: 宋体;">访问的协议控制（xml<span style="font-family: 宋体;">或者json<span style="font-family: 宋体;">等）。</span></span></span></span></span></span></span></span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">剥离开放api<span style="font-family: 宋体;">管理的功能和api<span style="font-family: 宋体;">的具体业务实现。</span></span></span></span></p>
<p> </p>
<p><strong>4）  <span style="font-family: 宋体;">总结</span></strong></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">由于时间的预算有限，以上内容均是对于目前基础服务各个平台的定位和架构方向的粗略阐述，也没有对文字重新校对；</span></span></p>
<p><span style="font-size: 18px;"><span style="font-family: 宋体;">因为未来业务的发展往往是多变的，故而基础服务的功能和方向也会不断的微调，但是总体的方向应该不会有所改变。希望粗略的文档能够让大家理解公司业务架构上的取舍和未来的演变方向。</span></span></p>
<p><span style="font-size: 6.5pt;"> </span></p>
<p><span style="font-size: 6.5pt;"> </span></p>
<p style="text-align: right;"><span style="font-size: 20px;">By <span style="font-family: 宋体;">车江毅</span></span></p>
<p>（备注：欢迎大家一起交流，分享，并指出架构的不足，tks！）</p>
<p><span style="color: #b22222;"><strong>开源QQ群: .net 开源基础服务  238543768</strong></span></p></div>