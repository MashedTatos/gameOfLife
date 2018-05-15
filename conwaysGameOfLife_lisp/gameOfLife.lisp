
(setq *print-case* :capitalize)

(print "How many generations?")
(defvar *gens* (read))

(defclass cell()
	((state
	:initarg :state :initform (error "Must have orig cell state"))
	(next-state
	:initarg :next-state :initform 1))
)


(defun get-state(cell)
	(slot-value cell 'state)
)

(defun set-state(cell state)
	(setf (slot-value cell 'state) state)
)

(defun get-next-state (cell)
	(slot-value cell 'next-state)
)
(defun set-next-state(cell next-state)
	(setf(slot-value cell 'next-state) next-state)
)

(defun get-random()
	(setf rand (round(random 1.0)))
)

(defvar *world* (make-array '(50 50)))


(defun init ()
	(dotimes(x 50)
		(dotimes(y 50)
			(setf *random-state* (make-random-state nil))
			(setf (aref *world* x y) (make-instance 'cell :state (get-random) :next-state 0))
				)))

(defun draw()
	(dotimes (x 50)
		(dotimes (y 50)
				(prin1(get-state (aref *world* x y)))	
				)
			(print "")
			))


(defun get-neighbors(row col)
	(defparameter sum 0)
	

	(setf sum (+ sum (get-neighbor-state row col 1 0)))
	(setf sum (+ sum (get-neighbor-state row col 0 1)))
	(setf sum (+ sum (get-neighbor-state row col 1 1)))

	(setf sum (+ sum (get-neighbor-state row col 0 (- 0 1))))
	(setf sum (+ sum (get-neighbor-state row col (- 0 1) 0)))
	(setf sum (+ sum (get-neighbor-state row col (- 0 1) (- 0 1))))

	(setf sum (+ sum (get-neighbor-state row col 1 (- 0 1))))
	(setf sum (+ sum (get-neighbor-state row col (- 0 1) 1))))
	


(defun calc-next-state(row col)
	(setf neighbors (get-neighbors row col))
	;(print neighbors)
	(setf cell (aref *world* row col))
	(setf cell-state (get-state cell))
	(if (= cell-state 1 )
		(cond
		((< neighbors 2) (set-next-state cell 0) )
		((or(= neighbors 2) (= neighbors 3))  (set-next-state cell 1))
		((> neighbors 3) (set-next-state cell 0)))
	)

	(if (= cell-state 0)
		(if(= neighbors 3) (set-next-state cell 1)))
	)




(defun get-neighbor-state(x y offsetx offsety)
	(setf height 50)
	(setf width 50)
	(setf row (mod(+ x (+ offsetx width)) width))
	(setf col (mod(+ y(+ offsety height)) height))

	(get-state (aref *world* row col))
	
	
)

(defun next-gen()
	(dotimes (x 50)
		(dotimes (y 50)
			(calc-next-state x y)
			)
		)
	(dotimes (x 50)
		(dotimes (y 50)
			(setf cell (aref *world* x y))
			(set-state cell (get-next-state cell))
			)
		)
)

(defun loop-game()
	(dotimes (x *gens*)

		(clear-screen)
		(draw)
		(format t "Generations left: ~a ~%" (- *gens* x))
		(sleep 0.2)
		(next-gen)
	)

	(print "Simulation finished!")
	
)
(defun clear-screen()
	(dotimes (x 60)
		(print "")
		
	)
)

(defun start()
	(init)
	(loop-game)
)
(start)



