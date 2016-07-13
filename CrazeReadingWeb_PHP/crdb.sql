


CREATE TABLE IF NOT EXISTS `crdb_rsssource` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `site_name` varchar(64) NOT NULL DEFAULT '' COMMENT '网站名称',
  `site_code` varchar(32) NOT NULL DEFAULT '' COMMENT '编码',
  `site_url` varchar(200) NOT NULL DEFAULT '' COMMENT '网站地址',
  `article_url_pattern` varchar(200) NOT NULL DEFAULT '' COMMENT '文章地址模式',
  `article_url_range` varchar(64) NOT NULL DEFAULT '' COMMENT '文章地址范围',
  `gather_interval` int(10) NOT NULL DEFAULT 0 COMMENT '采集时间间隔，单位秒',
  `last_visit_time` int(10) NOT NULL DEFAULT 0 COMMENT '上次访问时间',
  PRIMARY KEY (`id`)  
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=utf8;





CREATE TABLE IF NOT EXISTS `crdb_urlrule` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `article_url_pattern` varchar(200) NOT NULL DEFAULT '' COMMENT '网址模式',
  `article_content_csspath` varchar(64) NOT NULL DEFAULT '' COMMENT '正文css路径',
  PRIMARY KEY (`id`)  
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=utf8;


CREATE TABLE IF NOT EXISTS `crdb_article` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `article_link` varchar(200) NOT NULL DEFAULT '' COMMENT '文章链接',
  `article_title` varchar(200) NOT NULL DEFAULT '' COMMENT '文章标题',
  `article_content` text NOT NULL DEFAULT '' COMMENT '文章内容',
  `article_time` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '文章日期',
  PRIMARY KEY (`id`)  
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=utf8;

INSERT INTO `crdb_rsssource` (`site_name`, `site_code`, `site_url`, `article_url_pattern`, `article_url_range`, `gather_interval`) VALUES
('博客园首页', 'utf-8', 'http://www.cnblogs.com/', 'http://www.cnblogs.com/*/p/*.html', 'div#post_list','10')