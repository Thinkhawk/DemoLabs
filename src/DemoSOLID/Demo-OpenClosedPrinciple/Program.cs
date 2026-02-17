Console.WriteLine("Demo of OPEN CLOSED PRINCIPLE");

Demo01Test();

Console.WriteLine();

Demo02Test();


static void Demo01Test()
{
    double totalAmount = 10_000, 
           proposedAmount, 
           finalAmount;
    Demo_OpenClosedPrinciple.Demo01.Invoice invoice = new Demo_OpenClosedPrinciple.Demo01.Invoice();

    Console.WriteLine($"Invoice TOTAL Amount: {totalAmount}");

    proposedAmount = invoice.GetDiscount(
        totalAmount, 
        Demo_OpenClosedPrinciple.Demo01.InvoiceType.ProposedInvoice);
    Console.WriteLine($"Invoice PROPOSED Amount: {proposedAmount}");

    finalAmount = invoice.GetDiscount(
        totalAmount, 
        Demo_OpenClosedPrinciple.Demo01.InvoiceType.FinalInvoice);
    Console.WriteLine($"Invoice FINAL Amount: {finalAmount}");
}

static void Demo02Test()
{
    double totalAmount = 110_000,
           proposedAmount,
           finalAmount;
    Demo_OpenClosedPrinciple.Demo02.Invoice invoice;

    Console.WriteLine($"Invoice TOTAL Amount: {totalAmount}");

    invoice = new Demo_OpenClosedPrinciple.Demo02.ProposedInvoice();
    proposedAmount = invoice.GetDiscount(totalAmount);
    Console.WriteLine($"Invoice PROPOSED Amount: {proposedAmount}");

    invoice = new Demo_OpenClosedPrinciple.Demo02.FinalInvoice();
    finalAmount = invoice.GetDiscount(totalAmount);
    Console.WriteLine($"Invoice FINAL Amount: {finalAmount}");
}