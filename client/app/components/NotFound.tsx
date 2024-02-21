export default function NotFound() {
  return (
    <section className='bg-white dark:bg-gray-900'>
      <div className='mx-auto max-w-screen-xl px-4 py-8 lg:px-6 lg:py-16'>
        <div className='mx-auto max-w-screen-sm text-center'>
          <h1 className='dark:text-primary-500 mb-4 text-7xl font-extrabold tracking-tight text-dracula-pink lg:text-9xl'>
            404
          </h1>
          <p className='mb-4 text-3xl font-bold tracking-tight text-gray-900 dark:text-white md:text-4xl'>
            Alguma coisa está faltando.
          </p>
          <p className='mb-4 text-lg font-light text-gray-500 dark:text-gray-400'>
            Desculpe, não conseguimos encontrar essa página. Você encontrará
            muitas coisas para explorar na página inicial.{' '}
          </p>
          <a
            href='/'
            className='my-4 inline-flex rounded-lg bg-dracula-pink px-5 py-2.5 text-center text-sm font-medium text-white transition-all duration-500 ease-in-out hover:bg-dracula-pink-800 focus:outline-none focus:ring-4 focus:ring-dracula-pink dark:focus:ring-gray-400'
          >
            Página inicial
          </a>
        </div>
      </div>
    </section>
  )
}
