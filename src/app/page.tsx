import styles from './page.module.css'

export default function Home() {
  return (
    <main>
      <header>
        <a href="" className={styles.headerLinkLogo}>My Blog</a>
        <nav className={styles.headerNav}>
          <a href="">Login</a>
          <a href="">Register</a>
        </nav>
      </header>
      <div className={styles.divPost}>
        <div>
        <img src="https://picsum.photos/1920/1080" alt="hero" />
        </div>
        <div className={styles.divTexts}>
          <h2>Lorem ipsum dolor sit.</h2>
          <p className={styles.pInfo}>
            <a className={styles.linkAuthor}>Rian Negreiros</a>
            <time>2023-07-20 12:00</time>
          </p>
          <p className={styles.pSummary}>Lorem, ipsum dolor sit amet consectetur adipisicing elit. Magnam numquam mollitia sed praesentium, consequuntur enim libero, eum iure quis officiis ducimus, inventore consequatur voluptatibus. Accusamus explicabo voluptas animi minus officia.</p>
        </div>
      </div>
    </main>
  )
}
