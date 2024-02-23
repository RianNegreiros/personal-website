import Link from 'next/link'

export default function TermsOfService() {
  return (
    <div className='mx-auto my-8 max-w-2xl p-6 text-gray-800 shadow-md dark:text-gray-100'>
      <h2 className='mb-4 text-2xl font-bold'>1. Termos</h2>
      <p className='mb-4'>
        Ao acessar o site{' '}
        <Link href='https://riannegreiros.dev/' className='text-cyan-500'>
          riannegreiros.dev
        </Link>
        , você concorda em cumprir estes termos de serviço, todas as leis e
        regulamentos aplicáveis e concorda que é responsável pelo cumprimento de
        todas as leis locais aplicáveis. Se você não concordar com algum desses
        termos, está proibido de usar ou acessar este site. Os materiais
        contidos neste site são protegidos pelas leis de direitos autorais e
        marcas comerciais aplicáveis.
      </p>
      <h2 className='mb-4 mt-6 text-3xl font-bold'>2. Uso de Licença</h2>
      <p>
        É concedida permissão para baixar temporariamente uma cópia dos
        materiais (informações ou software) no site riannegreiros.dev, apenas
        para visualização transitória pessoal e não comercial. Esta é a
        concessão de uma licença, não uma transferência de título e, sob esta
        licença, você não pode:
      </p>
      <ol className='list-inside list-decimal'>
        <li>modificar ou copiar os materiais;</li>
        <li>
          usar os materiais para qualquer finalidade comercial ou para exibição
          pública (comercial ou não comercial);
        </li>
        <li>
          tentar descompilar ou fazer engenharia reversa de qualquer software
          contido no site riannegreiros.dev;
        </li>
        <li>
          remover quaisquer direitos autorais ou outras notações de propriedade
          dos materiais; ou
        </li>
        <li>
          transferir os materiais para outra pessoa ou &apos;espelhe&apos; os
          materiais em qualquer outro servidor.
        </li>
      </ol>
      <p>
        Esta licença será automaticamente rescindida se você violar alguma
        dessas restrições e poderá ser rescindida por riannegreiros.dev a
        qualquer momento. Ao encerrar a visualização desses materiais ou após o
        término desta licença, você deve apagar todos os materiais baixados em
        sua posse, seja em formato eletrônico ou impresso.
      </p>
      <h2 className='mb-4 mt-6 text-3xl font-bold'>
        3. Isenção de responsabilidade
      </h2>
      <ol className='list-inside list-decimal'>
        <li>
          <span>
            Os materiais no site da riannegreiros.dev são fornecidos &apos;como
            estão&apos;. riannegreiros.dev não oferece garantias, expressas ou
            implícitas, e, por este meio, isenta e nega todas as outras
            garantias, incluindo, sem limitação, garantias implícitas ou
            condições de comercialização, adequação a um fim específico ou não
            violação de propriedade intelectual ou outra violação de direitos.
          </span>
        </li>
        <li>
          <span>
            Além disso, o riannegreiros.dev não garante ou faz qualquer
            representação relativa à precisão, aos resultados prováveis ou à
            confiabilidade do uso dos materiais em seu site ou de outra forma
            relacionado a esses materiais ou em sites vinculados a este site.
          </span>
        </li>
      </ol>
      <h2 className='mb-4 mt-6 text-3xl font-bold'>4. Limitações</h2>
      <p>
        Em nenhum caso o riannegreiros.dev ou seus fornecedores serão
        responsáveis ​​por quaisquer danos (incluindo, sem limitação, danos por
        perda de dados ou lucro ou devido a interrupção dos negócios)
        decorrentes do uso ou da incapacidade de usar os materiais em
        riannegreiros.dev, mesmo que riannegreiros.dev ou um representante
        autorizado da riannegreiros.dev tenha sido notificado oralmente ou por
        escrito da possibilidade de tais danos. Como algumas jurisdições não
        permitem limitações em garantias implícitas, ou limitações de
        responsabilidade por danos consequentes ou incidentais, essas limitações
        podem não se aplicar a você.
      </p>
      <h2 className='mb-4 mt-6 text-3xl font-bold'>
        5. Precisão dos materiais
      </h2>
      <p>
        Os materiais exibidos no site da riannegreiros.dev podem incluir erros
        técnicos, tipográficos ou fotográficos. riannegreiros.dev não garante
        que qualquer material em seu site seja preciso, completo ou atual.
        riannegreiros.dev pode fazer alterações nos materiais contidos em seu
        site a qualquer momento, sem aviso prévio. No entanto, riannegreiros.dev
        não se compromete a atualizar os materiais.
      </p>
      <h2 className='mb-4 mt-6 text-3xl font-bold'>6. Links</h2>
      <p>
        O riannegreiros.dev não analisou todos os sites vinculados ao seu site e
        não é responsável pelo conteúdo de nenhum site vinculado. A inclusão de
        qualquer link não implica endosso por do site. O uso de qualquer site
        vinculado é por conta e risco do usuário.
      </p>
      <h3 className='mb-4 mt-6 text-2xl font-bold'>Modificações</h3>
      <p>
        O riannegreiros.dev pode revisar estes termos de serviço do site a
        qualquer momento, sem aviso prévio. Ao usar este site, você concorda em
        ficar vinculado à versão atual desses termos de serviço.
      </p>
      <h3 className='mb-2 text-xl font-bold'>Lei aplicável</h3>
      <p className='mb-4'>
        Estes termos e condições são regidos e interpretados de acordo com as
        leis do riannegreiros.dev e você se submete irrevogavelmente à
        jurisdição exclusiva dos tribunais naquele estado ou localidade.
      </p>
    </div>
  )
}
