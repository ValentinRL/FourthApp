import type { CSSProperties } from "react";
import type { CustomerDetails } from "../types/customer";

type CustomerDetailsBoxProps = {
    customer: CustomerDetails | null;
    loading: boolean;
};

export default function CustomerDetailsBox({
    customer,
    loading,
}: CustomerDetailsBoxProps) {
    if (loading) {
        return (
            <div style={boxStyle}>
                <p>Loading customer details...</p>
            </div>
        );
    }

    if (!customer) {
        return (
            <div style={boxStyle}>
                <p>Select a customer to view details.</p>
            </div>
        );
    }

    return (
        <div style={boxStyle}>
            <h2>{customer.companyName}</h2>

            <p>
                <strong>Customer ID:</strong> {customer.customerId}
            </p>

            <p>
                <strong>Contact:</strong> {customer.contactName ?? "-"}
            </p>

            <p>
                <strong>Title:</strong> {customer.contactTitle ?? "-"}
            </p>

            <p>
                <strong>Address:</strong> {customer.address ?? "-"}
            </p>

            <p>
                <strong>City:</strong> {customer.city ?? "-"}
            </p>

            <p>
                <strong>Postal Code:</strong> {customer.postalCode ?? "-"}
            </p>

            <p>
                <strong>Country:</strong> {customer.country ?? "-"}
            </p>

            <p>
                <strong>Phone:</strong> {customer.phone ?? "-"}
            </p>

            <h3>Orders</h3>

            {customer.orders.length === 0 ? (
                <p>No orders found for this customer.</p>
            ) : (
                <table border={1} cellPadding={8} style={{ borderCollapse: "collapse", margin: "0 auto" }}>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Total Items</th>
                            <th>Total Amount</th>
                        </tr>
                    </thead>

                    <tbody>
                        {customer.orders.map((order) => (
                            <tr key={order.orderId}>
                                <td>{order.orderId}</td>
                                <td>{order.totalItems}</td>
                                <td>{order.totalAmount.toFixed(2)}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    );
}

const boxStyle: CSSProperties = {
    border: "1px solid #ccc",
    padding: "16px",
    width: "320px",
    minHeight: "240px",
    borderRadius: "8px",
};