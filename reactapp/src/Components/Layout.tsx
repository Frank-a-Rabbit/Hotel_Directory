import React, { useState, useEffect } from 'react';

type LayoutProps = {
    children: React.ReactNode;
}

const Layout:React.FC<LayoutProps> = ({ children }) => {
    const [message, setMessage] = useState("Test Message");

    return (
        <main>
            {children}
        </main>
    );
};

export default Layout;