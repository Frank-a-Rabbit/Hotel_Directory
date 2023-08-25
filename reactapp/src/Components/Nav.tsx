import React from 'react';
import { Link } from 'react-router-dom';
import styles from '../Styles/Nav.module.css';

type navProps = {
    menuState: boolean;
}

const Nav:React.FC<navProps> = ({menuState}) => {
    const links = [{ name: 'Home', url: '/' }, { name: 'About', url: '/about' }, { name: 'Login', url: '/login' }, { name: 'Hotels', url: '/hotels'}];
    return (
        <nav className={`${styles.navCont} ${menuState ? styles.active : ''}`} id='mobile-menu'>
            <ul>
                {links.map((link, index) => {
                    return (
                        <li key={index}>
                            <Link to={link.url}>{link.name}</Link>
                        </li>
                    )
                }
                )}
            </ul>
        </nav>
    )
}

export default Nav;