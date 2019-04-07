using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital14_Lambda_03_ExpressionTrees
{
    using System.Linq.Expressions;

    class Capital14_Lambda_03_ExpressionTrees
    {

        static void Main(string[] args)
        {
            // msdn上手动创建 表达式树，并编译解析结果
            CreatEprsTreeByAPI();

            // 本例
            CreateExpressionTree();

            Console.ReadKey();
        }

        /// <summary>
        /// 构造“a+b”的表达式结构，并解析表达式树
        /// </summary>
        static void CreateExpressionTree()
        {
            // 表达式树参数
            ParameterExpression a = Expression.Parameter(typeof(int), "a");
            ParameterExpression b = Expression.Parameter(typeof(int), "b");

            // 表达式主体
            BinaryExpression be = Expression.Add(a, b);

            // 构造表达式树
            Expression<Func<int, int, int>> expressionTree = Expression.Lambda<Func<int, int, int>>(be, a, b);

            // 分析树结构
            BinaryExpression body = (BinaryExpression)expressionTree.Body;
            ParameterExpression left = (ParameterExpression)body.Left;
            ParameterExpression right = (ParameterExpression)body.Right;

            // 输出表达式树
            Console.WriteLine("表达式结构为："); Console.WriteLine(expressionTree);
            Console.WriteLine("表达式主体为："); Console.WriteLine(expressionTree.Body);
            Console.WriteLine("表达式左节点为：{0}{4} 节点类型为：{1}{4}{4} 表达式右节点为：{2}{4}" +
                " 节点类型为：{3}{4}", left.Name, left.Type, right.Name, right.Type, Environment.NewLine);
        }

        /// <summary>
        /// msdn上手动创建 表达式树，并编译解析结果
        /// </summary>
        static void CreatEprsTreeByAPI()
        {
            // Add the following using directive to your code file:  
            // using System.Linq.Expressions;  

            // Manually build the expression tree for   
            // the lambda expression num => num < 5.  
            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression five = Expression.Constant(5, typeof(int));
            BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
            Expression<Func<int, bool>> lambda1 =
                Expression.Lambda<Func<int, bool>>(
                    numLessThanFive,
                    new ParameterExpression[] { numParam });

            // 输出Lambda表达式树
            Console.WriteLine("输出Lambda表达式树(lambda1)：\t\t\t"+lambda1);

            // 编译运行一个表达式树
            var isLessThanFive = lambda1.Compile()(6);
            // 输出结果
            Console.WriteLine("编译表达式树（lambda1.Compile()(6)），结果是：\t" + isLessThanFive);

            Console.WriteLine();
        }
    }
}
