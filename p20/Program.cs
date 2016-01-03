using System;
using System.Text.RegularExpressions;

namespace p20 {
    class Tool {
        static Random random = new Random();
        private static readonly double EPS = 1E-8;
        internal static int count(int n, int d) {
            return ("" + n).Length - ("" + n).Replace("" + d, "").Length;
        }
        internal static bool prime(int n) {
            if (n == 2)
                return true;
            if (n % 2 == 0 || n < 2)
                return false;
            for (int c = 3; c * c <= n; c += 2)
                if (n % c == 0)
                    return false;
            return true;
        }
        internal static void search(double fx, double fy, double fz, int total, int money) {
            for (int x = 0; x <= total; x++)
                for (int y = 0; y <= total - x - y; y++) {
                    int z = total - x - y;
                    if (fequal(fx * x + fy * y + z * fz, money))
                        Console.WriteLine("{0},{1},{2}", x, y, z);
                }
        }

        internal static int doss(int start, int end) {
            return random.Next(start, end + 1);
        }

        internal static int lcm(int a, int b) {
            return a * b / gcd(a, b);
        }

        private static int gcd(int a, int b) {
            return b != 0 ? gcd(b, a % b) : a;
        }

        internal static int sum(int start, int end, int mod) {
            start = (start + mod - 1) / mod * mod;
            end = end / mod * mod;
            return (start + end) * ((end - start) / mod + 1) / 2;
        }

        internal static bool diffAll(params int[] a) {
            for (int i = 0; i < a.Length; i++)
                for (int j = i + 1; j < a.Length; j++)
                    if (a[i] == a[j])
                        return false;
            return true;
        }

        internal static bool allTrue(params bool[] b) {
            foreach (bool c in b)
                if (!c)
                    return false;
            return true;
        }

        internal static bool imply(bool p, bool q) {
            return !p || q;
        }

        internal static int bool2int(bool b) {
            return b ? 1 : 0;
        }

        internal static int sum(int[] a) {
            int tot = 0;
            foreach (int cur in a) tot += cur;
            return tot;
        }

        static bool checkInt(float f) {
            return fequal(f, (int)f);
        }

        static bool fequal(double f1, double f2) {
            return Math.Abs(f1 - f2) < EPS;
        }

        internal static bool checkInt(float[] f) {
            foreach (float cur in f)
                if (!checkInt(cur))
                    return false;
            return true;
        }
    }
    class P01 {
        //1.	随机产生一些1—100之间的整数，直到产生的数为50为止。
        internal static void main(string[] args) {
            int start = 1, end = 100, target = 50;
            int tot = 0;
            for (int cur = Tool.doss(start, end); ; cur = Tool.doss(start, end)) {
                ++tot;
                Console.Write(cur + ",");
                if (cur == target) {
                    break;
                }
            }
            Console.WriteLine("\ntot={0}", tot);
            Console.WriteLine();
        }

    }
    class P02 {
        //2.	计算1—1000之间能同时被3和5整除的整数的和。
        internal static void main(string[] args) {
            int start = 1, end = 1000, mod = Tool.lcm(3, 5);
            Console.WriteLine(Tool.sum(start, end, mod));
        }

    }

    class P03 {
        //3.	打印下列图形：   
        //    1   
        //   121   
        //  12321   
        //   121   
        //    1  
        internal static void main(string[] args) {
            int n = 9;
            for (int i = 1; i <= n; i++) {
                for (int j = 0; j < (n - i); j++)
                    Console.Write(" ");
                for (int j = 1; j <= i; j++)
                    Console.Write(j);
                for (int j = i - 1; j >= 1; j--)
                    Console.Write(j);
                Console.WriteLine();
            }
            for (int i = n - 1; i >= 1; i--) {
                for (int j = 0; j < (n - i); j++)
                    Console.Write(" ");
                for (int j = 1; j <= i; j++)
                    Console.Write(j);
                for (int j = i - 1; j >= 1; j--)
                    Console.Write(j);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
    class P04 {
        //4.	一百匹马驮一百块瓦，一匹大马可以驮3块，一匹母马可驮2块，小马2匹可驮1块。试编程求需要各种马多少匹？  
        internal static void main(string[] args) {
            Tool.search(3, 2, 1.0 / 2, 100, 100);
        }
    }
    class P05 {
        //5.	有三种纪念邮票，第一种每套一张售价2元，第二种每套一张售价4元，第三种每套9张售价2元。现用100元买了100张邮票，问这三种邮票各买几张？ 
        internal static void main(string[] args) {
            Tool.search(2, 4, 2.0 / 9, 100, 100);
        }
    }
    class P06 {
        //6.	赵、钱、孙、李、周五人围着一张圆桌吃饭。饭后，周回忆说：“吃饭时，赵坐在钱旁边，钱的左边是孙或李”；李回忆说：“钱坐在孙左边，我挨着孙坐”。结果他们一句也没有说对。请问，他们在怎样坐的？ 
        static bool left(int p, int q) {
            return p + 1 == q || p == 4 && q == 0;
        }
        static bool right(int p, int q) {
            return left(q, p);
        }
        static bool near(int p, int q) {
            return left(p, q) || right(p, q);
        }
        internal static void main(string[] args) {
            int zhao = 0, qian, sun, li, zhou;
            for (qian = 1; qian <= 4; qian++)
                for (sun = 1; sun <= 4; sun++)
                    for (li = 1; li <= 4; li++)
                        for (zhou = 1; zhou <= 4; zhou++)
                            if (Tool.diffAll(qian, sun, li, zhou)) {
                                bool zp = near(zhao, qian), zq = left(sun, qian) || left(li, qian);
                                bool lp = left(qian, sun), lq = near(li, sun);
                                if (Tool.allTrue(!zp, !zq, !lp, !lq))
                                    Console.WriteLine("{0} {1} {2} {3} {4}", zhao, qian, sun, li, zhou);
                            }
            Console.WriteLine();
        }
    }
    class P07 {
        //7.	找数。一个三位数，各位数字互不相同，十位数字比个位、百位数字之和还要大，且十位、百位数字之和不是质数。编程找出所有符合条件的三位数。   
        //注：1.   不能手算后直接打印结果。   
        //  2.   “质数”即“素数”，是指除1和自身外，再没有其它因数大于1的自然数。
        internal static void main(string[] args) {
            for (int n = 100; n <= 999; n++) {
                int a = n / 100, b = (n % 100) / 10, c = n % 10;
                if (Tool.diffAll(a, b, c) && b > a + c && !Tool.prime(a + b))
                    Console.Write(n + " ");
            }
            Console.WriteLine();
        }

    }
    class P08 {
        //8.	选人。一个小组共五人，分别为A、B、C、D、E。现有一项任务，要他们中的3个人去完成。已知：（1）A、C不能都去；（2）B、C不能都不去；（3）如果C去了，D、E就只能去一个，且必须去一个；（4）B、C、D不能都去；（5）如果B去了，D、E就不能都去。编程找出此项任务该由哪三人去完成的所有组合。 
        internal static void main(string[] args) {
            for (int n = 0; n < (1 << 5); n++) {
                int a = n & 1, b = (n >> 1) & 1, c = (n >> 2) & 1, d = (n >> 3) & 1, e = n >> 4;
                if (Tool.allTrue(a + b + c + d + e == 3, a + c < 2, b + c > 0, Tool.imply(c == 1, d + e == 1), b + c + d < 3, Tool.imply(b == 1, d + e < 2))) {
                    Console.WriteLine("{0} {1} {2} {3} {4}", a, b, c, d, e);
                }
            }
            Console.WriteLine();
        }
    }
    class P09 {
        //  输入一个字符串，内有数字和非数字字符。如A123X456Y7A，302ATB567BC，打印字符串中所有连续（指不含非数字字符）的数字所组成的整数，并统计共有多少个整数。
        internal static void main(string[] args) {
            string[] s = Regex.Split("A123X456Y7A，302ATB567BC", "\\D+");
            foreach (string t in s)
                if (t.Trim().Length > 0)
                    Console.WriteLine(t); Console.WriteLine();
        }

    }
    class P10 {
        //10.	A、B、C三人进入决赛，赛前A说：“B和C得第二，我得第一”；B说：“我进入前两名，丙得第三名”；C说：“A不是第二，B不是第一”。比赛产生了一、二、三名，比赛结果显示：获得第一的选手全说对了，获得第二的选手说对了一句，获得第三的选手全说错了。编程求出A、B、C三名选手的名次。
        internal static void main(string[] args) {
            for (int a = 1; a <= 3; a++)
                for (int b = 1; b <= 3; b++)
                    for (int c = 1; c <= 3; c++) {
                        if (Tool.diffAll(a, b, c)) {
                            bool ap = (b == 2 && c == 2), aq = (a == 1);
                            bool bp = (b <= 2), bq = (c == 3);
                            bool cp = (a != 2), cq = (b != 1);
                            if (Tool.allTrue(a + Tool.bool2int(ap) + Tool.bool2int(aq) == 3,
                                b + Tool.bool2int(bp) + Tool.bool2int(bq) == 3,
                                c + Tool.bool2int(cp) + Tool.bool2int(cq) == 3
                                ))
                                Console.WriteLine("{0} {1} {2}", a, b, c);
                        }
                    }
            Console.WriteLine();
        }
    }
    class P11 {
        //11.	甲、乙、丙、丁四人共有糖若干块，甲先拿出一些糖分给另外三人，使他们三人的糖数加倍；乙拿出一些糖分给另外三人，也使他们三人的糖数加倍；丙、丁也照此办理，此时甲、乙、丙、丁四人各有16块，编程求出四个人开始各有糖多少块。  
        internal static void trans(int[] a) {
            for (int i = 0; i < a.Length; i++) {
                a[i] -= Tool.sum(a) - a[i];
                for (int j = 1; j < a.Length; j++)
                    a[(i + j) % a.Length] <<= 1;
            }
        }
        internal static void main(string[] args) {
            int N = 64;
            for (int a = N / 2; a <= N; ++a)
                for (int b = 0; b <= N - a; b++)
                    for (int c = 0; c <= N - a - b; c++) {
                        int d = N - a - b - c;
                        int[] A = { a, b, c, d };
                        trans(A);
                        if (Tool.allTrue(4 * A[0] == N, 4 * A[1] == N, 4 * A[2] == N, 4 * A[3] == N))
                            Console.WriteLine("{0} {1} {2} {3}", a, b, c, d);
                    }
            Console.WriteLine();
        }
    }
    class P12 {
        //12.	截数问题:   任意一个自然数，我们可以将其平均截取成三个自然数。例如自然数135768，可以截取成13,57,68三个自然数。如果某自然数不能平均截取(位数不能被3整除)，可将该自然数高位补零后截取。现编程从键盘上输入一个自然数N(N的位数<12)，计算截取后第一个数加第三个数减第二个数的结果
        static void op(int n) {
            string s = "00" + n;
            int w = s.Length / 3;
            int head = int.Parse(s.Substring(s.Length - 3 * w, w));
            int middle = int.Parse(s.Substring(s.Length - 2 * w, w));
            int tail = int.Parse(s.Substring(s.Length - w, w));
            Console.WriteLine("{0}+{2}-{1}={3}", head, middle, tail, head + tail - middle);
        }
        internal static void main(string[] args) {
            op(1);
            op(12);
            op(123);
            op(134567);
            op(1345678);
        }
    }
    class P13 {
        //13.	从键盘输入一段英文，将其中的英文单词分离出来：已知单词之间的分隔符包括空格、   问号、句号(小数点)和分号。    例如：输入：There   are   apples;   oranges   and   peaches   on   the   table.   
        //      输出：there
        //      are
        //apples
        //oranges
        //and
        //peaches
        //on
        //the
        //table
        internal static void main(string[] args) {
            foreach (string cur in Regex.Split("There   are   apples;   oranges   and   peaches   on   the   table. ", "\\W+"))
                Console.WriteLine(cur.Trim());
        }
    }
    class P14 {
        //       14.	山乡希望小学收到一箱捐赠图书，邮件上署名是“兴华中学高二班”，山乡希望小学校 长送来了感谢信，可是兴华中学高二年级有四个班，校长找来了四个班的班长，问他们是哪 个班做的这件好事。一班的班长说：“是四班做的。”二班的班长说：“是三班做的好事。”三 班的班长说：“不是我们班。”   四班的班长说：“三班的班长说的不对。”     
        //四个班的班长都说不是自己班做的，这就难坏了校长，后来得知四个班的班长中有两个 说得是真话，有两个没有说真话，请你利用计算机的逻辑判断编一个程序，找出究竟是哪个 班做了这件好事。不能手算后直接打印结果。
        internal static void main(string[] args) {
            for (int n = 0; n < (1 << 4); n++) {
                int c1 = n & 1, c2 = (n >> 1) & 1, c3 = (n >> 2) & 1, c4 = n >> 3;
                bool b1 = (c4 == 1), b2 = (c3 == 1), b3 = (c3 == 0), b4 = (!b3);
                if (Tool.bool2int(b1) + Tool.bool2int(b2) + Tool.bool2int(b3) + Tool.bool2int(b4) == 2 && c1 + c2 + c3 + c4 == 1)
                    Console.WriteLine("{0} {1} {2} {3}", c1, c2, c3, c4);
            }
            Console.WriteLine();
        }
    }
    class P15 {
        //15.	A，B，C，D，E五个人合伙夜间捕鱼，凌晨时都疲惫不堪，各自在河边的树丛中找地 方睡着了，日上三竿，E第一个醒来，他将鱼数了数，平分成五分，把多余的一条扔进河中，   拿走一份回家去了，D第二个醒来，他并不知道有人已经走了,照样将鱼平分成五分，又扔掉多余的一条，拿走自己的一份，接着C，B，A依次醒来，也都按同样的办法分鱼(平分成 五份，扔掉多余的一条，拿走自己的一份)，问五人至少合伙捕到多少条鱼。     也许你能用数学办法推出鱼的条数，但我们的要求你编出一个程序，让计算机帮你算出鱼的总数。   
        internal static void main(string[] args) {
            float[] f = new float[6];
            for (int n = 1; ; n++) {
                f[0] = n;
                for (int i = 1; i <= 5; i++)
                    f[i] = (f[i - 1] - 1) * 4 / 5;
                if (Tool.checkInt(f)) {
                    Console.WriteLine("{0},{1},{2},{3},{4},{5}", f[0], f[1], f[2], f[3], f[4], f[5]);
                    break;
                }
            }
            Console.WriteLine();
        }
    }
    class P16 {
        //16.	试编程找出能被各位数字之和整除的一切两位数。
        internal static void main(string[] args) {
            for (int n = 10; n <= 99; n++) {
                if (n % (n / 10 + n % 10) == 0)
                    Console.Write(n + " ");
            }
            Console.WriteLine();
        }

    }
    class P17 {
        // 17.	一个正整数的个位数字是6，如果把个位数字移到首位,所得到的数是原数的4倍，试编程找出满足条件的最小正整数。
        internal static void main(string[] args) {
            for (int n = 16; ; n += 10) {
                int m = int.Parse("6" + n / 10);
                if (m == 4 * n) {
                    Console.WriteLine("{0}={1}*4", m, n);
                    break;
                }
            }
            Console.WriteLine();
        }
    }
    class P18 {
        //18.	某本书的页码从1开始，小明算了算，总共出现了202个数1，试编程求这本书一共有多少页？   

        internal static void main(string[] args) {
            int tot = 0, TOT = 202, page;
            for (page = 1; tot < TOT; ++page)
                tot += Tool.count(page, 1);
            if (tot == TOT)
                Console.WriteLine(page);
            else
                Console.WriteLine("Impossible");
        }
    }
    class P19 {
        //      19.	从键盘上输入两个不超过32767的整数，试编程序用竖式加法形式显示计算结果。   
        //例如:   输入   123,   85   
        //显示:   　123   
        //+   　85   
        //　　-------------   
        //　　208   
        internal static void main(string[] args) {
            //   string[] s = Console.ReadLine().Split();
            int a = 12345, b = 6;
            Console.Write("{0,6}\n+{1,5}\n------\n{2,6}\n", a, b, a + b);
        }
    }
    class P20 {
        //20.	有30个男人女人和小孩同在一家饭馆进餐，共花了五十先令，其中男宾3先令，女宾2先令，小孩1先令。试编程求出男人女人小孩各多少人？   
        internal static void main(string[] args) {
            Tool.search(3, 2, 1, 30, 50);
        }
    }

    class Program {
        static void Main(string[] args) {
            P01.main(args);
            P02.main(args);
            P03.main(args);
            P04.main(args);
            P05.main(args);
            P06.main(args);
            P07.main(args);
            P08.main(args);
            P09.main(args);
            P10.main(args);
            P11.main(args);
            P12.main(args);
            P13.main(args);
            P14.main(args);
            P15.main(args);
            P16.main(args);
            P17.main(args);
            P18.main(args);
            P19.main(args);
            P20.main(args);
        }
    }
}
