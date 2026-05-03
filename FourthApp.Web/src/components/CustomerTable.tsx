
import type { Customer } from "../types/customer";

type CustomerTableProps = {
    customers: Customer[];
    loading: boolean;
    selectedCustomerId: string | null;
    onSelectCustomer: (customerId: string) => void;
};

export default function CustomerTable({
    customers,
    loading,
    selectedCustomerId,
    onSelectCustomer,
}: CustomerTableProps) {
    if (loading) {
        return <p>Loading customers...</p>;
    }

    return (
        <table border={1} cellPadding={8} style={{ borderCollapse: "collapse" }}>
            <thead>
                <tr>
                    <th>Customer ID</th>
                    <th>Company Name</th>
                    <th>Orders Count</th>
                </tr>
            </thead>

            <tbody>
                {customers.map((customer) => (
                    <tr
                        key={customer.customerId}
                        onClick={() => onSelectCustomer(customer.customerId)}
                        style={{
                            cursor: "pointer",
                            backgroundColor:
                                selectedCustomerId === customer.customerId ? "#eee" : "white",
                        }}
                    >
                        <td>{customer.customerId}</td>
                        <td>{customer.companyName}</td>
                        <td>{customer.ordersCount ?? "-"}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}