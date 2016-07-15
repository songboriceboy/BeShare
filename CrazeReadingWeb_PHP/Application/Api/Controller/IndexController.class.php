<?php

namespace Api\Controller;
use Think\Controller;

class IndexController extends Controller {
    
    public function get_one_task()
    {
        if(IS_POST){
            $list = M('')->Query("select * from crdb_rsssource order by gather_interval limit 0, 1");
            if(count($list) > 0)
            {
                $info = array('response'=>'get_one_task','code'=>0,'msg'=>'成功','data'=>$list);
                echo json_encode($info);exit;
            }
            else
            {
                $info = array('response'=>'get_one_task','code'=>1,'msg'=>'失败','data'=>array());
                echo json_encode($info);exit;
            }
        }
    }
    public function get_one_link()
    {
        if(IS_POST){
            $list = M('')->Query("select id,article_link from crdb_article where article_content is null or article_content = '' order by article_time asc limit 0, 1");
            if(count($list) > 0)
            {
                $w['id'] = $list[0]['id'];
                $data['is_visit'] = 1;
                M('article')->where($w)->save($data);
                $info = array('response'=>'get_one_link','code'=>0,'msg'=>'成功','data'=>$list);
                echo json_encode($info);exit;
            }
            else
            {
                $info = array('response'=>'get_one_link','code'=>1,'msg'=>'失败','data'=>array());
                echo json_encode($info);exit;
            }
        }
    }
    public function get_all_rules()
    {
        if(IS_POST){
            $list = M('')->Query("select * from crdb_urlrule");
            if(count($list) > 0)
            {
                $info = array('response'=>'get_all_rules','code'=>0,'msg'=>'成功','data'=>$list);
                echo json_encode($info);exit;
            }
            else
            {
                $info = array('response'=>'get_all_rules','code'=>1,'msg'=>'失败','data'=>array());
                echo json_encode($info);exit;
            }
        }
    }
    public function is_link_exist()
    {
        if(IS_POST){
            $offset1 = I('post.offset1');
            $offset2 = I('post.offset2');
            $list = M('')->Query("select * from crdb_article where bloom_offset1 = '%s' and bloom_offset2 = '%s'", $offset1, $offset2);
            if(count($list) > 0)
            {
                $info = array('response'=>'get_all_rules','code'=>1);
                echo json_encode($info);exit;
            }
            else
            {
                $info = array('response'=>'get_all_rules','code'=>0);
                echo json_encode($info);exit;
            }
        }
    }
    public function add_article()
    {
        //这里还应该做一下排重，2个offset应该传过来
        if(IS_POST){
            $article_content = I('post.article_content');


            $decode_content = htmlspecialchars_decode($article_content);

            $decode_json_content = json_decode($decode_content);

            foreach ($decode_json_content as $key => $val) {

                $data[$key] = $val;


            }

            $offset1 = $data['bloom_offset1'];
            $offset2 = $data['bloom_offset2'];

            $list = M('')->Query("select * from crdb_article where bloom_offset1 = '%s' and bloom_offset2 = '%s'", $offset1, $offset2);
            if(count($list) == 0)
            {
                M('article')->add($data);
            }

        }
    }

    public function update_article_content()
    {
        if(IS_POST){
            $w['id'] = I('post.id');
            $data['article_content'] = I('post.content');
            M('article')->where($w)->save($data);
        }
    }

}