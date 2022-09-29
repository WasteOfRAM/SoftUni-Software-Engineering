function solve() {
    const questionAnswers = {
        'Question #1': 'onclick',
        'Question #2': 'JSON.stringify()',
        'Question #3': 'A programming API for HTML and XML documents'
    }

    let corectAnswers = 0;

    let quiz = document.getElementById('quizzie');

    quiz.addEventListener('click', choseAnswer);

    function choseAnswer(e) {
        if (e.target.classList[0] !== "answer-text") {
            return;
        }

        let currentQuestionTag = e.target
            .parentElement
            .parentElement
            .parentElement
            .parentElement;

        let question = currentQuestionTag
            .querySelector('.question-wrap')
            .textContent.split(':')[0].trim();


        let userAwnser = e.target.textContent;

        if (userAwnser === questionAnswers[question]) {
            corectAnswers++;
        }

        currentQuestionTag.style.display = 'none';
        currentQuestionTag.nextElementSibling.style.display = 'block';

        if (currentQuestionTag.nextElementSibling.tagName === 'UL') {
            currentQuestionTag.nextElementSibling.lastElementChild.lastElementChild.textContent =
                corectAnswers === 3 ? "You are recognized as top JavaScript fan!"
                    : `You have ${corectAnswers} right answers`
        }
    }
}