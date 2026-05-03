import { useEffect, useState } from "react";
import { getCustomerById, getCustomers } from "../api/customersApi";
import CustomerDetailsBox from "../components/CustomerDetailsBox";
import CustomerTable from "../components/CustomerTable";
import type { CustomerDetails, Customer } from "../types/customer";

export default function CustomersPage() {
    const [customers, setCustomers] = useState<Customer[]>([]);
    const [selectedCustomer, setSelectedCustomer] =
        useState<CustomerDetails | null>(null);

    const [page, setPage] = useState<number>(1);
    const [pageSize, setPageSize] = useState<number>(10);
    const [search, setSearch] = useState<string>("");

    const [totalCount, setTotalCount] = useState<number>(0);

    const [loadingCustomers, setLoadingCustomers] = useState<boolean>(false);
    const [loadingDetails, setLoadingDetails] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const totalPages = Math.ceil(totalCount / pageSize);

    useEffect(() => {
        async function loadCustomers() {
            try {
                setLoadingCustomers(true);
                setError(null);

                const result = await getCustomers({
                    page,
                    pageSize,
                    search,
                });

                setCustomers(result.customers);
                setTotalCount(result.totalCount);

                setSelectedCustomer(null);
            } catch (err) {
                setError(
                    err instanceof Error ? err.message : "Failed to load customers"
                );
            } finally {
                setLoadingCustomers(false);
            }
        }

        loadCustomers();
    }, [page, pageSize, search]);

    async function handleCustomerSelect(customerId: string) {
        try {
            setLoadingDetails(true);
            setError(null);

            const result = await getCustomerById(customerId);
            setSelectedCustomer(result);
        } catch (err) {
            setError(
                err instanceof Error ? err.message : "Failed to load customer details"
            );
        } finally {
            setLoadingDetails(false);
        }
    }

    function handleSearchChange(value: string) {
        setSearch(value);
        setPage(1);
    }

    function handlePageSizeChange(value: number) {
        setPageSize(value);
        setPage(1);
    }

    return (
        <div style={{ padding: "24px" }}>
            <h1>Customers</h1>

            {error && <p style={{ color: "red" }}>{error}</p>}

            <div style={{ marginBottom: "16px" }}>
                <input
                    value={search}
                    onChange={(event) => handleSearchChange(event.target.value)}
                    placeholder="Search customers..."
                    style={{ padding: "8px", width: "240px" }}
                />

                <select
                    value={pageSize}
                    onChange={(event) =>
                        handlePageSizeChange(Number(event.target.value))
                    }
                    style={{ marginLeft: "12px", padding: "8px" }}
                >
                    <option value={5}>5</option>
                    <option value={10}>10</option>
                    <option value={25}>25</option>
                    <option value={50}>50</option>
                </select>
            </div>

            <p>
                Page {page} of {totalPages || 1} — showing {customers.length} of{" "}
                {totalCount} customers
            </p>

            <div style={{ display: "flex", gap: "24px", alignItems: "flex-start" }}>
                <CustomerTable
                    customers={customers}
                    loading={loadingCustomers}
                    selectedCustomerId={selectedCustomer?.customerId ?? null}
                    onSelectCustomer={handleCustomerSelect}
                />

                <CustomerDetailsBox
                    customer={selectedCustomer}
                    loading={loadingDetails}
                />
            </div>

            <div style={{ marginTop: "16px" }}>
                <button
                    disabled={page <= 1 || loadingCustomers}
                    onClick={() => setPage((current) => current - 1)}
                >
                    Previous
                </button>

                <button
                    disabled={page >= totalPages || loadingCustomers}
                    onClick={() => setPage((current) => current + 1)}
                    style={{ marginLeft: "8px" }}
                >
                    Next
                </button>
            </div>
        </div>
    );
}