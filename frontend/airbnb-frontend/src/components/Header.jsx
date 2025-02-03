import { Link } from "react-router-dom";
import styles from "../styles/Header.module.css";

const Header = ({ onOpenModal }) => { // Получаем функцию из App.jsx
  return (
    <header className={styles.header}>
      <div className={styles.logo}>
        <Link to="/">HomeFU</Link>
      </div>

      <div className={styles.searchBar}>
        <input type="text" placeholder="Поиск жилья..." />
        <button>🔍</button>
      </div>

      <nav className={styles.nav}>
        <Link to="/listings">Каталог</Link>
        <Link to="/about">О нас</Link>
        <Link to="/contact">Контакты</Link>
        <button className={styles.signIn} onClick={onOpenModal}>Войти</button> {/* Вызываем onOpenModal */}
      </nav>
    </header>
  );
};

export default Header;