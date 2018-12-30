#01 Component Pattern 组件模式

## 01.01- Intent 意义
Allow a single entity to **span** multiple domains without **coupling** the domains to each other

允许一个单一的实体跨越多个不同域而不会导致耦合。

## 01.02- The Pattern 模式描述
A single entity spans multiple domains. To keep the domains **isolated**, the code for each is placed in its own component class. The entity is **reduced** to a simple container of components.
<br>
单一实体横跨了多个域。为了能够保持域之间相互隔离，每个域的代码都独立地放在自己的组件类中。实体本身则可以简化为这些组件的容器。

## 01.03- When to Use It 使用情形
Components are most commonly found within the core class ***that defines the entities in a game***, but they may be useful in other places as well. This pattern can be put to good use when any of these are true:

- You have a class that touches multiple domains which you want to keep decoupled from each other.
- A class is getting massive and hard to work with.
- You want to be able to define a variety of objects that share different capabilities, but using inheritance doesn’t let you pick the parts you want to reuse precisely enough.

组件最常见于游戏中定义实体的核心类，但是它们也能够用在别的地方。当如下条件成立时，组件模式就能够发挥它的作用 :

- 你有一个涉及多个域的类，但是你希望这些域保持相互解耦。
- 一个类越来越庞大，越来越难以开发。
- 你希望定义许多共享不同能力的对象，但采用继承的办法却无法令你精确地重用代码。

## 01.04- Tips
Unity引擎的主要设计正是围绕组件模型来进行的。
## 01.05- 我的思考
1. 两个关键词 “实体”、“域”。
2. 实体只有一个，是这些“域”的容器。
3. 每个域有不同的能力，域之间没有任何联系
4. “暗影飞行人”就是这种模式

----------

#02  Data Locality Pattern 数据局部性模式

## 02.01- Intent 意义
Accelerate memory access by **arranging** data to take advantage of CPU caching.
<br>
通过合理组织数据利用CPU的缓存机制来**加快内存访问速度**。

## 02.02- The Pattern 模式描述

Modern CPUs have caches to speed up memory access. These can access memory **adjacent** to recently accessed memory much quicker. Take advantage of that to improve performance by increasing data locality — keeping data in contiguous memory in the order that you process it.

现代的CPU有缓存来加速内存读取，其可以更快地读取最近访问过的内存毗邻的内存。基于这一点，我们通过保证处理的数据排列在连续内存上，以提高内存局部性，从而提高性能。

为了保证数据局部性，就要避免缓存不命中。也许你需要牺牲一些宝贵的抽象。你越围绕数据局部性设计程序，就越放弃继承、接口和它们带来的好处。没有银弹，只有权衡。

## 02.03- When to Use It 使用情形

- 遇到性能问题时使用

	 使用数据局部性的第一准则是在遇到性能问题时使用。不要将其应用在代码库不经常使用的角落上。 优化代码后其结果往往更加复杂，更加缺乏灵活性。

- 性能差是由缓存不命中引发的

	就本模式而言，还得确认你的性能问题确实是由**缓存不命中而引发的**。如果代码是因为其他原因而缓慢，这个模式自然就不会有帮助。
- 使用工具profilers

	简单的性能评估方法是手动添加指令，用计时器检查代码中两点间消耗的时间。而为了找到糟糕的缓存使用情况，知道缓存不命中有多少发生，又是在哪里发生的，则需要使用更加复杂的工具—— **profilers**。

组件模式是为优化缓存的最常见例子。而任何需要接触很多数据的关键代码，考虑数据局部性都是很重要的。

## 02.04- 我的理解
1. 提高内存局部性，从而加快访问速度，提升性能
2. 一块缓存的容量假如是100个房间，你要的数据放在第1、13、51、88、99个房间中，访问第2个房间没有取到要用的数据就是不命中；使用“数据局部性模式”后变为，你要的数据会放在第1、2、3、4，或者第16、17、18、19，或者51、52、53、54个房间中。


----------

# 03 Dirty Flag Pattern 脏标记模式

## 03.01  Intent 意义

Avoid unnecessary work by deferring it until the result is needed.

将工作推迟到必要时进行，以避免不必要的工作。

## 03.02 The Pattern 模式描述

A set of primary data changes over time. A set of derived data is determined from this using some expensive process. A “dirty” flag tracks when the derived data is out of sync with the primary data. It is set when the primary data changes. If the flag is set when the derived data is needed, then it is reprocessed and the flag is cleared. Otherwise, the previous cached derived data is used.

一组原始数据随时间变化。一组颜色数据经过一些**代价昂贵的操作**由这些数据确定。一个脏标记跟踪这个衍生数据是否和原始数据同步。它在原始数据改变时被设置。如果它被设置了，那么当需要衍生数据时，它们就会被重新计算并且标记被清除。**否则**就使用缓存的数据。


## 03.03 When to Use It 使用情形

就像其他优化模式一样，此模式会增加代码复杂度。只在有足够大的性能问题时，再考虑使用这一模式。

脏标记在这两种情况下适用：

- 当前任务有昂贵的**计算开销**
- 当前任务有昂贵的**同步开销**。

若满足这两者之一，也就是两者从原始数据转换到目标数据会消耗很多时间，都可以考虑使用脏标记模式来节省开销。

若原始数据的变化速度远高于目标数据的使用速度，此时数据会因为随后的修改而失效，此时就不适合使用脏标记模式。

## 03.04 我的收获
### 04.01 是怎么实现优化的？
### 04.02 何时使用
### 04.03 关键词
1. 代价昂贵的操作
2. 用于优化，是优化模式的一种
3. 原始数据，目标数据


