<?php

$arr = array(

	/* 数据库设置 */

    'DB_TYPE'               =>  'mysql',     // 数据库类型
    'DB_HOST'               =>  'localhost', // 服务器地址
    'DB_NAME'               =>  'crdb',     // 数据库名
    'DB_USER'               =>  'root',      // 用户名
    'DB_PWD'                =>  '',          // 密码
    'DB_PORT'               =>  '',          // 端口
    'DB_PREFIX'             =>  'crdb_',    // 数据库表前缀

    'SESSION_PREFIX'        =>  'crdb_', 	 // session 前缀

    'COOKIE_PREFIX'         =>  'crdb_',    // Cookie前缀 避免冲突

    'TMPL_L_DELIM'          =>  '<{',            // 模板引擎普通标签开始标记
    'TMPL_R_DELIM'          =>  '}>',            // 模板引擎普通标签结束标记
    'site_url' => 'http://app.douyuehan.com',

);

return $arr;

?>