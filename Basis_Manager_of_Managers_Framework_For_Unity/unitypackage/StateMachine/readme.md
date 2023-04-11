# 演示

![](.\StateMachineExample.gif)

# 一些说明

unity - version 2020.3.X (LTS) 

URP（for Slash VFX）

使用的小人均来自@明日方舟Arknight

https://prts.wiki/

红刀哥以及安多恩的素材来自于

https://space.bilibili.com/560082484/channel/collectiondetail?sid=192445

Spine-Unity运行时文档

http://zh.esotericsoftware.com/spine-unityhttp://zh.esotericsoftware.com/spine-unity

使用的资源：

[Stylized Slash VFX | VFX 粒子 | Unity Asset Store](https://assetstore.unity.com/packages/vfx/particles/stylized-slash-vfx-200233)

[Stylized Elemental Auras/Status VFX | VFX | Unity Asset Store](https://assetstore.unity.com/packages/vfx/stylized-elemental-auras-status-vfx-218621)

[Blades & Bludgeoning Free Sample Pack | 音频 音效 | Unity Asset Store](https://assetstore.unity.com/packages/audio/sound-fx/blades-bludgeoning-free-sample-pack-179306)

我只制作了红刀哥的状态机

控制方法如下：

- AD左右移动

- 鼠标左键攻击

- 按住空格释放技能

- 按e是复活

- 按q是去世

其他两个小人只是将其导入并没有为其制作控制，你可以仿照我的实现为她们添加。

# 注意事项

明日方舟的Spine小人是使用Spine来制作的

请保持一致的版本

## 安装Spine-Unity 3.8依赖

版本很重要！！！

它没有往下版本的兼容，非常奇怪。

在导入到unity之前，找到Spine中的skel骨骼以及atlas依赖做后缀的修改

->将Spine skeleton的skel后缀改为“.skel.bytes”后缀

->将Spine atlas的atlas后缀改为“.atlas.txt”后缀

导入成功：

![](https://cdn.nlark.com/yuque/0/2023/png/28556534/1680017141535-d03aba88-bf6d-4c10-9282-a4e77c7df7f6.png)

## 创建使用动画机控制的游戏物体

拖动XxxData文件到unity界面，然后会出现下面的三个选项。

![](https://cdn.nlark.com/yuque/0/2023/png/28556534/1680019453768-275f2e6e-3a69-43f0-b4bc-6c526b3d0965.png)

选择最下面那个用于创建状态机控制的动画。

这个操作将会自动创建一个游戏物体并为其添加Animator和Skeleton Mecanim组件，此外还会自动添加骨骼信息并为其Animator创建controller，在controller下面生成它的动画。

![](https://cdn.nlark.com/yuque/0/2023/png/28556534/1680019582560-8dcf1508-9847-443b-bb16-e747bdb30787.png)

最后面附上一个修改material颜色的方法：

因为没有受击动画（如果你想实现的话），所有就想到了以前的做法：用协程使材质闪红后返回到原来的材质。

但是由于使用的shader并不是unity的builtin管线，

要去查找Spine官方文档查找相应的解决方案。

其实在SpineExample中也有相应的示例：找到Material相关的浏览即可。

![](https://cdn.nlark.com/yuque/0/2023/png/28556534/1681143102619-e8eb89af-fa8d-451e-bcf6-df537f24d38d.png)
