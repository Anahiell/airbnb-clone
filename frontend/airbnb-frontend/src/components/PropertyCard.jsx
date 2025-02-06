import styles from "../styles/PropertyCard.module.css";

const PropertyCard = ({ id, image, title, location, price, rating }) => {
  return (
    <div className={styles.card}>
      <img src={image} alt={title} className={styles.image} />
      <div className={styles.info}>
        <h3>{title}</h3>
        <p>{location}</p>
        <div className={styles.footer}>
          <span className={styles.price}>{price} / ночь</span>
          <span className={styles.rating}>⭐ {rating}</span>
        </div>
      </div>
    </div>
  );
};

export default PropertyCard;
