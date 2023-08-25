import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from '../Styles/Header.module.css';
import Nav from './Nav';

type HeaderProps = {
    logo?: React.ReactNode;
}

const Header:React.FC<HeaderProps> = ({ logo }) => {
    const history = useNavigate();
    const [menuActive, setMenuActive] = useState(false);
    useEffect(() => {
        setMenuActive(false);
    }, [history]);

    return (
        <header className={styles.header}>
            <div className={styles.inner}>
                {logo ?
                    <div className={styles.logo}>
                        {logo}
                    </div>
                    :     
                    <div className={styles.logo}>
                        <img loading='lazy' src={`${process.env.PUBLIC_URL}/assets/logo-default.png`} alt="Hotel Directory" />
                    </div>
                }
                <div className={styles.navCont}>
                    <Nav menuState={menuActive} />
                </div>
            </div>
            <button className={styles.hammy} aria-haspopup='true' aria-controls='mobile-menu' onClick={() => setMenuActive(!menuActive)}>
                <div></div>
                <div></div>
                <div></div>
            </button>
        </header>
    )
}

export default Header;