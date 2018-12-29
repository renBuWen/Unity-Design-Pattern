# Data Locality Pattern 数据局部性模式

## 01- Intent 意义
Accelerate memory access by **arranging** data to take advantage of CPU caching.
<br>
通过合理组织数据利用CPU的缓存机制来**加快内存访问速度**。

## 02- The Pattern 模式描述

Modern CPUs have caches to speed up memory access. These can access memory **adjacent** to recently accessed memory much quicker. Take advantage of that to improve performance by increasing data locality — keeping data in contiguous memory in the order that you process it.

现代的CPU有缓存来加速内存读取，其可以更快地读取最近访问过的内存毗邻的内存。基于这一点，我们通过保证处理的数据排列在连续内存上，以提高内存局部性，从而提高性能。

为了保证数据局部性，就要避免缓存不命中。也许你需要牺牲一些宝贵的抽象。你越围绕数据局部性设计程序，就越放弃继承、接口和它们带来的好处。没有银弹，只有权衡。

## 03- When to Use It 使用情形

- 遇到性能问题时使用

	 使用数据局部性的第一准则是在遇到性能问题时使用。不要将其应用在代码库不经常使用的角落上。 优化代码后其结果往往更加复杂，更加缺乏灵活性。

- 性能差是由缓存不命中引发的

	就本模式而言，还得确认你的性能问题确实是由**缓存不命中而引发的**。如果代码是因为其他原因而缓慢，这个模式自然就不会有帮助。
- 使用工具profilers

	简单的性能评估方法是手动添加指令，用计时器检查代码中两点间消耗的时间。而为了找到糟糕的缓存使用情况，知道缓存不命中有多少发生，又是在哪里发生的，则需要使用更加复杂的工具—— **profilers**。

组件模式是为优化缓存的最常见例子。而任何需要接触很多数据的关键代码，考虑数据局部性都是很重要的。

## 04- 我的理解
1. 提高内存局部性，从而加快访问速度，提升性能
2. 一块缓存的容量假如是100个房间，你要的数据放在第1、13、51、88、99个房间中，访问第2个房间没有取到要用的数据就是不命中；使用“数据局部性模式”后变为，你要的数据会放在第1、2、3、4，或者第16、17、18、19，或者51、52、53、54个房间中。


