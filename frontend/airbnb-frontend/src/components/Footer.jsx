import styles from "../styles/Footer.module.css";

const Footer = () => {
  return (
    <footer className={styles.footer}>
      <div className={styles.links}>
        <a href="/privacy">Политика конфиденциальности</a>
        <a href="/terms">Условия использования</a>
      </div>

      <p>&copy; 2024 HomeFU. Все права защищены.</p>

      <div className={styles.social}>
        <a href="https://facebook.com">📘</a>
        <a href="https://twitter.com">🐦</a>
        <a href="https://instagram.com">📷</a>
      </div>
    </footer>
  );
};

export default Footer;
