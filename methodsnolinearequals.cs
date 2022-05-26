double fo(double x)
{
	return (x-1)*sin(x)+1;
}
double f_d(double x)
{
	return x*sin(x)+(x-1)*cos(x);
}
void half_interval(double a, double b)
{
	cout << "Метод половинного интервала" << endl;
	double c = (a + b) / 2;
	while (abs(b - a) > E)
	{
		if (fo(c) == 0)
		{
			cout << "Корень уравнения: " << c << endl;
			return;
		}
		else
		{
			if (fo(c)*fo(a) < 0)
			{
				b = c;
			}
			else
			{
				a = c;
			}
		}
		c = (a + b) / 2;
	}
	cout << "Корень уравнения: " << c << endl;
	cout << endl;
}

void n_tangent()
{
	cout << "Метод Ньютона" << endl;
	double x = 3.0, x1;
	x1 = x - fo(x) / f_d(x);
	while (abs(fo(x) / f_d(x)) > E)
	{
		x = x1;
		x1 = x - fo(x) / f_d(x);
	}
	cout << "Корень уравнения: " << x << endl;
	cout << endl;
}