interface UnauthorizedProps {
  errorMessage: string
}

export default function Unauthorized({ errorMessage }: UnauthorizedProps) {
  return (
    <section className='bg-white dark:bg-gray-900'>
      <div className='mx-auto max-w-screen-xl px-4 py-8 lg:px-6 lg:py-16'>
        <div className='mx-auto max-w-screen-sm text-center'>
          <h1 className='mb-4 text-7xl font-extrabold tracking-tight text-dracula-red dark:text-dracula-purple lg:text-9xl'>
            401
          </h1>
          <p className='text-dracula-foreground mb-4 text-3xl font-bold tracking-tight dark:text-dracula-cyan md:text-4xl'>
            Acesso não autorizado
          </p>
          <p className='mb-4 text-lg font-light text-gray-500 dark:text-gray-400'>
            {errorMessage}
          </p>
        </div>
      </div>
    </section>
  )
}
